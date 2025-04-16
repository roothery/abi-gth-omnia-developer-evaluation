using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core.
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleRepository.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new sale in the database.
        /// </summary>
        /// <param name="sale">The sale to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale.</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <summary>
        /// Retrieves a sale by its unique identifier, including items.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale if found, null otherwise.</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a sale by its sale number, including items.
        /// </summary>
        /// <param name="saleNumber">The sale number to search for.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale if found, null otherwise.</returns>
        public async Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
        }

        /// <summary>
        /// Retrieves all sales from the database, including items.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all sales.</returns>
        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sales.Include(s => s.Items).ToListAsync(cancellationToken);
        }

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
        public async Task<PaginatedList<Sale>> GetListAsync(string? saleNumber = null, bool? isCanceled = null,
            Branch? branch = null, Customer? customer = null, DateTime? startSaleDate = null, DateTime? endSaleDate = null,
            int page = 0, int pageSize = 0, string? sortBy = null, bool isDesc = false, CancellationToken cancellationToken = default)
        {
            var query = _context.Sales
                .Where(x =>
                    (saleNumber == null || x.SaleNumber.Contains(saleNumber))
                    && (isCanceled == null || isCanceled == x.IsCancelled)
                    && (branch == null || branch == x.Branch)
                    && (customer == null || customer == x.Customer)
                    && (startSaleDate == null || x.SaleDate >= startSaleDate)
                    && (endSaleDate == null || x.SaleDate <= endSaleDate)
                ).Include(s => s.Items);

            return await PaginatedList<Sale>.CreateAsync(query, page, pageSize, sortBy, isDesc, cancellationToken);
        }

        /// <summary>
        /// Updates an existing sale in the database.
        /// </summary>
        /// <param name="sale">The sale to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // <summary>
        /// Deletes a sale from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the sale was deleted, false if not found.</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);

            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Retrieves a sale item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale item if found, null otherwise.</returns>
        public async Task<SaleItem?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing sale item in the database.
        /// </summary>
        /// <param name="saleItem">The sale item to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task UpdateItemAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            _context.SaleItems.Update(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes a sale item from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the sale item was deleted, false if not found.</returns>
        public async Task<bool> DeleteItemAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetItemByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.SaleItems.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
