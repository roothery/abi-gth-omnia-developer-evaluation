using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Creates a new sale in the repository.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale.</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale if found, null otherwise.</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its sale number.
        /// </summary>
        /// <param name="saleNumber">The sale number associated with the sale.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale if found, null otherwise.</returns>
        Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all sales from the repository.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all sales.</returns>
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of sales from the database with optional filters, sorting, and related items.
        /// </summary>
        /// <param name="saleNumber">Optional filter by sale number.</param>
        /// <param name="isCanceled">Optional filter by cancellation status.</param>
        /// <param name="branch">Optional filter by branch.</param>
        /// <param name="customer">Optional filter by customer.</param>
        /// <param name="startSaleDate">Optional start date for sale date filter.</param>
        /// <param name="endSaleDate">Optional end date for sale date filter.</param>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="sortBy">The field to sort by.</param>
        /// <param name="isDesc">Indicates whether sorting should be descending.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of sales matching the specified criteria, including related sale items.</returns>
        Task<PaginatedList<Sale>> GetListAsync(string? saleNumber = null, bool? isCanceled = null,
            Branch? branch = null, Customer? customer = null, DateTime? startSaleDate = null, DateTime? endSaleDate = null,
            int page = 0, int pageSize = 0, string? sortBy = null, bool isDesc = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing sale in the repository.
        /// </summary>
        /// <param name="sale">The sale to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to be deleted.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the sale was successfully deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale item if found; otherwise, null.</returns>
        Task<SaleItem?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing sale item in the repository.
        /// </summary>
        /// <param name="saleItem">The sale item to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateItemAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale item from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item to be deleted.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the sale item was successfully deleted, false otherwise.</returns>
        Task<bool> DeleteItemAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
