using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the GetSalesHandler.
    /// </summary>
    public class GetSalesHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSalesHandler _handler;

        public GetSalesHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSalesHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a request without filters returns all sales paginated.
        /// </summary>
        [Fact(DisplayName = "Returns paginated sales without filters")]
        public async Task Handle_WithoutFilters_ReturnsPaginatedSales()
        {
            // Given
            var command = new GetSalesCommand { Page = 1, PageSize = 5 };
            var sales = GetSalesHandlerTestData.GenerateValidFakeSales();
            var paged = new PaginatedList<Sale>
            {
                Items = sales.Take(command.PageSize).ToList(),
                Page = command.Page,
                PageSize = command.PageSize,
                TotalCount = sales.Count
            };

            _saleRepository.GetListAsync(
                Arg.Any<string>(),
                Arg.Any<bool?>(),
                Arg.Any<Branch?>(),
                Arg.Any<Customer?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<DateTime?>(),
                command.Page,
                command.PageSize,
                Arg.Any<string>(),
                Arg.Any<bool>(),
                Arg.Any<CancellationToken>()).Returns(paged);

            var expected = GetSalesHandlerTestData.GenerateGetSalesResults(sales.Take(command.PageSize).ToList());
            _mapper.Map<List<GetSalesResult>>(paged.Items).Returns(expected);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Items.Should().NotBeNullOrEmpty();
            result.Items.Should().HaveCount(command.PageSize);
            result.Page.Should().Be(command.Page);
            result.PageSize.Should().Be(command.PageSize);
            result.TotalCount.Should().Be(sales.Count);
        }

        /// <summary>
        /// Tests that pagination parameters return the correct page of sales.
        /// </summary>
        [Fact(DisplayName = "Returns correct page when paginating")]
        public async Task Handle_WithPagination_ReturnsCorrectPage()
        {
            // Given
            var command = new GetSalesCommand { Page = 2, PageSize = 3 };
            var sales = GetSalesHandlerTestData.GenerateValidFakeSales(10);

            var paged = new PaginatedList<Sale>
            {
                Items = sales.Skip((command.Page - 1) * command.PageSize).Take(command.PageSize).ToList(),
                Page = command.Page,
                PageSize = command.PageSize,
                TotalCount = sales.Count
            };

            _saleRepository.GetListAsync(
                command.SaleNumber,
                command.IsCanceled,
                command.Branch,
                command.Customer,
                command.StartSaleDate,
                command.EndSaleDate,
                command.Page,
                command.PageSize,
                command.SortBy,
                command.IsDesc,
                Arg.Any<CancellationToken>())
                .Returns(paged);

            var expected = GetSalesHandlerTestData.GenerateGetSalesResults(paged.Items);
            _mapper.Map<List<GetSalesResult>>(paged.Items).Returns(expected);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Items.Should().HaveCount(command.PageSize);
            result.Page.Should().Be(command.Page);
            result.TotalCount.Should().Be(sales.Count);
        }

        /// <summary>
        /// Tests that filtering by branch returns only sales from that branch.
        /// </summary>
        [Fact(DisplayName = "Returns sales filtered by branch")]
        public async Task Handle_FilterByBranch_ReturnsFilteredSales()
        {
            // Given
            var command = new GetSalesCommand
            {
                Branch = Branch.BranchC,
                Page = 1,
                PageSize = 10
            };

            var sales = GetSalesHandlerTestData.GenerateValidFakeSales(20).Where(s => s.Branch == Branch.BranchC).ToList();

            var paged = new PaginatedList<Sale>
            {
                Items = sales,
                Page = command.Page,
                PageSize = command.PageSize,
                TotalCount = sales.Count
            };

            _saleRepository.GetListAsync(
                command.SaleNumber,
                command.IsCanceled,
                command.Branch,
                command.Customer,
                command.StartSaleDate,
                command.EndSaleDate,
                command.Page,
                command.PageSize,
                command.SortBy,
                command.IsDesc,
                Arg.Any<CancellationToken>()).Returns(paged);

            var expected = GetSalesHandlerTestData.GenerateGetSalesResults(paged.Items);
            _mapper.Map<List<GetSalesResult>>(paged.Items).Returns(expected);

            //When
            var result = await _handler.Handle(command, CancellationToken.None);

            //Then
            result.Items.Should().OnlyContain(i => i.Branch == Branch.BranchC);
            result.TotalCount.Should().Be(sales.Count);
        }

        /// <summary>
        /// Tests that a request with no matching sales returns an empty result.
        /// </summary>
        [Fact(DisplayName = "Returns empty result when no sales found")]
        public async Task Handle_NoSalesFound_ReturnsEmpty()
        {
            // Given
            var command = new GetSalesCommand { Page = 1, PageSize = 10 };

            var paged = new PaginatedList<Sale>
            {
                Items = new List<Sale>(),
                Page = command.Page,
                PageSize = command.PageSize,
                TotalCount = 0
            };

            _saleRepository.GetListAsync(
                Arg.Any<string>(),
                Arg.Any<bool?>(),
                Arg.Any<Branch?>(),
                Arg.Any<Customer?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<DateTime?>(),
                command.Page,
                command.PageSize,
                Arg.Any<string>(),
                Arg.Any<bool>(),
                Arg.Any<CancellationToken>()).Returns(paged);

            var expected = GetSalesHandlerTestData.GenerateGetSalesResults(paged.Items);
            _mapper.Map<List<GetSalesResult>>(paged.Items).Returns(new List<GetSalesResult>());

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
        }
    }
}
