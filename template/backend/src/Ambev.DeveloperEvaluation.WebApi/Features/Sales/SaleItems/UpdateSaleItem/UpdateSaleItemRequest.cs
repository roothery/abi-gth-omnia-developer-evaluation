using System.ComponentModel;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Represents a request to update sale item in the system.
    /// </summary>
    public class UpdateSaleItemRequest
    {
        /// <summary>
        /// Gets the unique identifier for the sale item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the identifier of the associated sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets the product associated with the sale item.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets the quantity of the product purchased.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets a value indicating whether the item was cancelled.
        /// </summary>
        [DefaultValue(false)]
        public bool IsCancelled { get; set; }
    }
}
