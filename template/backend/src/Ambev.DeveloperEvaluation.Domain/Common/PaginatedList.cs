using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> query,
            int page = 1,
            int pageSize = 10,
            string? sortBy = null,
            bool isDesc = false,
            CancellationToken cancellationToken = default)
        {
            
            page = page < 1 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                try
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.PropertyOrField(parameter, sortBy);
                    var lambda = Expression.Lambda(property, parameter);

                    string method = isDesc ? "OrderByDescending" : "OrderBy";

                    var expression = Expression.Call(
                        typeof(Queryable),
                        method,
                        new[] { typeof(T), property.Type },
                        query.Expression,
                        Expression.Quote(lambda)
                    );

                    query = query.Provider.CreateQuery<T>(expression);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Pagination] Failed to sort by '{sortBy}': {ex.Message}");
                }
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<T>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
