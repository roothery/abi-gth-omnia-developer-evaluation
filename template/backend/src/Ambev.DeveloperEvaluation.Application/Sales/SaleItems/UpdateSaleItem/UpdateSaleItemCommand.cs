using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Command for update a update sale item.
    /// </summary>
    public class UpdateSaleItemCommand
    {
        /// <summary>
        /// Gets the unique identifier for the sale item.
        /// </summary>
        public Guid Id { get; set; }

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
