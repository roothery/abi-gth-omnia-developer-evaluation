using System.ComponentModel;
using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SaleItems.UpdateSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Represents a request to update sale in the system.
    /// </summary>
    public class UpdateSaleRequest
    {
        /// <summary>
        /// Gets the unique identifier for the sale.
        /// </summary>
        //public Guid Id { get; set; }

        /// <summary>
        /// Gets the unique identifier for the sale.
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
        public List<UpdateSaleItemRequest> Items { get; set; } = new();

        /// <summary>
        /// Gets a value indicating whether the sale was cancelled.
        /// </summary>
        [DefaultValue(false)]
        public bool IsCancelled { get; set; }
    }
}
