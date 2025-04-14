using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a sale,
    /// including Sale number, sale date, customer, branch, status canceled, sale items.
    /// It implements <see cref="IRequest" /> to initiate the request
    /// that returns a <see cref="CreateSaleResult" />.
    /// The data provided in this command is validated using the
    /// <see cref="CreateSaleCommandValidator" /> which extends
    /// <see cref="AbstractValidator{T}" /> to ensure that the fields are correctly
    /// populated and follow the required rules.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets or sets the sale number of the sale to be created.
        /// </summary>
        public string SaleNumber { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the date the sale was created.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the sale.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        public Branch Branch { get; set; }

        /// <summary>
        /// Gets or sets the list of items included in the sale.
        /// </summary>
        public List<CreateSaleItemCommand> Items { get; set; } = new();

        /// <summary>
        /// Gets or sets a value indicating whether the sale was cancelled.
        /// </summary>
        public bool IsCancelled { get; set; } = false;
    }
}
