using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Command for creating a update sale.
    /// </summary>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Gets the unique identifier for the sale.
        /// </summary>
        public Guid Id { get; set; }

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
        public List<UpdateSaleItemCommand> Items { get; set; } = new();

        /// <summary>
        /// Gets a value indicating whether the sale was cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}
