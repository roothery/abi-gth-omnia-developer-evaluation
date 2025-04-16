using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the CancelSaleHandler.
    /// </summary>
    public class CancelSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly CancelSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the CancelSaleHandlerTests.
        /// Sets up the test dependencies.
        /// </summary>
        public CancelSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _bus = Substitute.For<IBus>();
            _handler = new CancelSaleHandler(_saleRepository, _mapper, _bus);
        }

        /// <summary>
        /// Tests that a valid cancel sale command results in a successful cancellation.
        /// </summary>
        [Fact(DisplayName = "Given valid cancel command When cancelling sale Then operation is successful")]
        public async Task Handle_ValidCommand_CancelsSale()
        {
            // Given
            var saleId = Guid.NewGuid();
            var sale = new Sale { Id = saleId };
            var command = new CancelSaleCommand(saleId);
            var result = new CancelSaleResult { Id = saleId };

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<CancelSaleResult>(sale).Returns(result);

            // When
            var response = await _handler.Handle(command, CancellationToken.None);

            // Then
            response.Should().Be(result);
            response.Id.Should().Be(result.Id);
            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
            await _bus.Received(1).Publish(Arg.Any<SaleCancelled>());
        }

        /// <summary>
        /// Tests that an invalid cancel sale request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid cancel command When cancelling sale Then throws validation exception")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new CancelSaleCommand(Guid.Empty);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that a non-existent sale ID throws a KeyNotFoundException.
        /// </summary>
        [Fact(DisplayName = "Given non-existing sale id When cancelling sale Then throws KeyNotFoundException")]
        public async Task Handle_NonExistingSale_ThrowsKeyNotFoundException()
        {
            // Given
            var saleId = Guid.NewGuid();
            var command = new CancelSaleCommand(saleId);

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale?)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"sale with ID {saleId} not found");
        }
    }
}
