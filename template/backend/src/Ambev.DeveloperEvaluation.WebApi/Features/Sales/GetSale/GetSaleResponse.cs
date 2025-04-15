using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SaleItems.GetSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// API response model for GetSale operation.
    /// </summary>
    public class GetSaleResponse
    {
        /// <summary>
        /// Gets the unique identifier for the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the unique sale number for the sale.
        /// </summary>
        public string SaleNumber { get; set; } = String.Empty;

        /// <summary>
        /// Gets the date the sale was created.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets the customer associated with the sale.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets the branch where the sale was made.
        /// </summary>
        public Branch Branch { get; set; }

        /// <summary>
        /// Gets the list of items included in the sale.
        /// </summary>
        public List<GetSaleItemResponse> Items { get; set; } = new();

        /// <summary>
        /// Gets a value indicating whether the sale was cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets the total monetary amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
