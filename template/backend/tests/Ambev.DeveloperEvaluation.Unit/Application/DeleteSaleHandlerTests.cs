using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the DeleteSaleHandler.
    /// </summary>
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBus _bus;
        private readonly DeleteSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the DeleteSaleHandlerTests.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _bus = Substitute.For<IBus>();
            _handler = new DeleteSaleHandler(_saleRepository, _bus);
        }

        /// <summary>
        /// Tests that a valid sale ID results in a successful deletion.
        /// </summary>
        [Fact(DisplayName = "Given valid sale id When deleting sale Then operation is successful")]
        public async Task Handle_ValidId_DeletesSale()
        {
            // Given
            var saleId = Guid.NewGuid();
            _saleRepository.DeleteAsync(saleId, Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));

            // When
            var result = await _handler.Handle(new DeleteSaleCommand { Id = saleId }, CancellationToken.None);

            // Then
            result.Success.Should().BeTrue();
            await _saleRepository.Received(1).DeleteAsync(saleId, Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid delete sale request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale id When deleting sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new DeleteSaleCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that a non-existent sale ID throws an KeyNotFoundException.
        /// </summary>
        [Fact(DisplayName = "Given non-existing sale id When deleting sale Then throws KeyNotFoundException")]
        public async Task Handle_NonExistingId_ThrowsKeyNotFoundException()
        {
            // Given
            var saleId = Guid.NewGuid();
            _saleRepository.DeleteAsync(saleId, Arg.Any<CancellationToken>()).Returns(Task.FromResult(false));

            // When
            var act = () => _handler.Handle(new DeleteSaleCommand { Id = saleId }, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
