using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for processing UpdateSaleCommand requests.
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of UpdateSaleHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the UpdateSaleCommand command.
        /// </summary>
        /// <param name="command">The UpdateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The result of the update operation.</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingSale == null)
                throw new InvalidOperationException($"Sale with ID {command.Id} not found");

            var sale = _mapper.Map(command, existingSale);
            UpdateSaleItems(sale, command);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var result = _mapper.Map<UpdateSaleResult>(sale);
            return result;

        }

        /// <summary>
        /// Updates the sale items with new values and applies business rules.
        /// </summary>
        private void UpdateSaleItems(Sale sale, UpdateSaleCommand command)
        {
            var incomingSaleItems = command.Items.ToDictionary(i => i.Id);

            sale.Items = sale.Items
                .Where(item => incomingSaleItems.ContainsKey(item.Id))
                .ToList();

            foreach (var item in command.Items)
            {
                var isNewItem = item.Id == Guid.Empty;
                if (isNewItem)
                {
                    AddNewItem(sale, item);
                    continue;
                }

                var existingSaleItem = sale.Items.SingleOrDefault(i => i.Id == item.Id);
                if (existingSaleItem != null)
                {
                    existingSaleItem.Update(
                        item.SaleId,
                        item.Product,
                        item.Quantity,
                        item.UnitPrice,
                        item.IsCancelled
                    );
                }
                else
                {
                    AddNewItem(sale, item);
                }
            }
        }

        private static void AddNewItem(Sale sale, UpdateSaleItemCommand item)
        {
            sale.Items.Add(new SaleItem(
                item.SaleId,
                item.Product,
                item.Quantity,
                item.UnitPrice,
                item.IsCancelled
            ));
        }
    }
}
