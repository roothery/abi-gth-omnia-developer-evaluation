using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of DeleteSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        public DeleteSaleHandler(ISaleRepository saleRepository, IBus bus)
        {
            _saleRepository = saleRepository;
            _bus = bus;
        }

        /// <summary>
        /// Handles the DeleteSaleCommand request.
        /// </summary>
        /// <param name="command">The GetSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="validator">The validator for GetSaleCommand.</param>
        /// <returns>The sale and items details.</returns>
        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _saleRepository.DeleteAsync(command.Id, cancellationToken);
            if (!result)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

            await _bus.Publish(new SaleDeleted(command.Id, DateTime.UtcNow));

            return new DeleteSaleResult { Success = result };

        }
    }
}
