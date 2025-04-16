using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of CancelSaleHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        public CancelSaleHandler(ISaleRepository saleRepository, IMapper mapper, IBus bus)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _bus = bus;
        }

        /// <summary>
        /// Handles the CancelSaleCommand request
        /// </summary>
        /// <param name="command">The CancelSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns true if the sale has been cancelled.</returns>
        public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"sale with ID {command.Id} not found");

            sale.Cancel();

            await _saleRepository.UpdateAsync(sale, cancellationToken);
            await _bus.Publish(new SaleCancelled(sale.Id, DateTime.UtcNow));

            return _mapper.Map<CancelSaleResult>(sale);
        }
    }
}
