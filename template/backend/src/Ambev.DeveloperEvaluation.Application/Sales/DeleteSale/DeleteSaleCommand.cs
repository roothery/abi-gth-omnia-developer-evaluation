using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Command for retrieving a sale.
    /// </summary>
    /// <remarks>
    /// This command is used to delete a sale from Id.
    /// </remarks>
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
