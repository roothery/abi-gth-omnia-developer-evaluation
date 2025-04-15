using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Represents a request to retrieve a paginated list of sales with optional filters and sorting.
    /// </summary>
    public class GetSalesCommand : IRequest<PaginatedList<GetSalesResult>>
    {
        /// <summary>
        /// Filters by sale number (optional).
        /// </summary>
        public string? SaleNumber { get; set; }

        /// <summary>
        /// Filters by cancellation status (optional).
        /// </summary>
        public bool? IsCanceled { get; set; }

        /// <summary>
        /// Filters by branch (optional).
        /// </summary>
        public Branch? Branch { get; set; }

        /// <summary>
        /// Filters by customer (optional).
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// Filters sales from this date forward (optional).
        /// </summary>
        public DateTime? StartSaleDate { get; set; }

        /// <summary>
        /// Filters sales up to this date (optional).
        /// </summary>
        public DateTime? EndSaleDate { get; set; }

        /// <summary>
        /// The page number for pagination. Default is 0 (returns all results).
        /// </summary>
        public int Page { get; set; } = 0;

        /// <summary>
        /// The number of items per page. Default is 0 (returns all results).
        /// </summary>
        public int PageSize { get; set; } = 0;

        /// <summary>
        /// Field name to sort results by (optional).
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Whether sorting is descending. Default is false (ascending).
        /// </summary>
        public bool IsDesc { get; set; }
    }
}
