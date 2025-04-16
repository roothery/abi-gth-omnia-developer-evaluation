using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
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
    /// Contains unit tests for the UpdateSaleHandler.
    /// </summary>
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;
        
        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateSaleHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid update sale command returns a result with the updated sale ID.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When updating sale Then returns updated sale ID")]
        public async Task Handle_ValidCommand_ReturnsUpdatedSaleId()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            var existingSale = new Sale { Id = command.Id };
            var mappedSale = new Sale { Id = command.Id };
            var result = new UpdateSaleResult { Id = command.Id };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingSale);
            _mapper.Map(command, existingSale).Returns(mappedSale);
            _mapper.Map<UpdateSaleResult>(mappedSale).Returns(result);

            // When
            var updateResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            updateResult.Should().NotBeNull();
            updateResult.Id.Should().Be(command.Id);
            await _saleRepository.Received(1).UpdateAsync(mappedSale, Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid update sale command throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale data When updating sale Then throws validation exception")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new UpdateSaleCommand();

            // When
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that updating a non-existent sale throws an invalid operation exception.
        /// </summary>
        [Fact(DisplayName = "Given non-existing sale ID When updating sale Then throws invalid operation exception")]
        public async Task Handle_NonExistingSale_ThrowsInvalidOperationException()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns((Sale)null);

            // When
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Sale with ID {command.Id} not found");
        }

        /// <summary>
        /// Tests that a new item in the command is added to the sale during update.
        /// </summary>
        [Fact(DisplayName = "Given new sale item When updating sale Then adds item to the sale")]
        public async Task Handle_NewItem_AddsItemToSale()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();
            var saleId = command.Id;

            var existingSale = new Sale
            {
                Id = saleId,
                SaleNumber = command.SaleNumber,
                Items = new List<SaleItem>()
            };

            var mappedSale = new Sale
            {
                Id = saleId,
                SaleNumber = command.SaleNumber,
                Items = new List<SaleItem>()
            };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(existingSale);

            _mapper.Map(command, existingSale).Returns(mappedSale);
            _mapper.Map<UpdateSaleResult>(mappedSale).Returns(new UpdateSaleResult
            {
                Id = saleId
            });

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(saleId);
            mappedSale.Items.Count.Should().BeGreaterThan(0);
        }

    }
}
