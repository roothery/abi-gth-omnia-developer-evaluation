using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Command for retrieving a sale.
    /// </summary>
    public class CancelSaleCommand : IRequest<CancelSaleResult>
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of CancelSaleCommand
        /// </summary>
        public CancelSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
