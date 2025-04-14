using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Command for creating a new sale item.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating an individual sale item,
    /// including the associated sale ID, product, quantity, unit price, discount, and status.
    /// It implements <see cref="IRequest{TResponse}" /> to initiate the request
    /// that returns a <see cref="CreateSaleItemResult" />.
    /// The data provided in this command can be validated using
    /// a corresponding validator to ensure that the item meets all business rules
    /// such as valid quantity ranges, pricing, and discount application.
    /// </remarks>
    public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
    {
        /// <summary>
        /// Gets or sets the identifier of the associated sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the product associated with the sale item.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product purchased.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item was cancelled.
        /// </summary>
        public bool IsCancelled { get; set; } = false;
    }
}
