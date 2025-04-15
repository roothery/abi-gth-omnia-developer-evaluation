using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the GetSaleHandler.
    /// </summary>
    public class GetSaleHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly GetSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the GetSaleHandlerTests.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public GetSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSaleHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid sale id returns the corresponding sale result.
        /// </summary>
        [Fact(DisplayName = "Given valid sale id When getting sale Then returns sale result")]
        public async Task Handle_ValidId_ReturnsSaleResult()
        {
            // Given
            var command = GetSaleHandlerTestData.GenerateValidCommand();
            var sale = GetSaleHandlerTestData.GenerateValidFakeSale();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);

            var saleResult = GetSaleHandlerTestData.CreateSaleResult(sale);
            _mapper.Map<GetSaleResult>(sale).Returns(saleResult);

            // When
            var getSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            getSaleResult.Should().NotBeNull();
            getSaleResult.Id.Should().Be(sale.Id);
            getSaleResult.SaleNumber.Should().Be(sale.SaleNumber);
            await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid get sale request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale id When getting sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new GetSaleCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        ///     Tests that a non-existing sale id throws an InvalidOperationException.
        /// </summary>
        [Fact(DisplayName = "Given non-existing sale id When getting sale Then throws InvalidOperationException")]
        public async Task Handle_NonExistingId_ThrowsInvalidOperationException()
        {
            // Given
            var command = GetSaleHandlerTestData.GenerateValidCommand();

            // Simulate no existing sale found
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns((Sale)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Sale with ID {command.Id} not found");
        }
    }
}
