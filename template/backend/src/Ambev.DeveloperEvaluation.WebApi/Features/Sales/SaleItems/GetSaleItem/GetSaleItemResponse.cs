using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SaleItems.GetSaleItem
{
    /// <summary>
    /// API response model for sale items in GetSale operation.
    /// </summary>
    public class GetSaleItemResponse
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
        /// Gets the discount applied to this item based on quantity rules.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets a value indicating whether the item was cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets the total amount for the item after applying discount.
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
