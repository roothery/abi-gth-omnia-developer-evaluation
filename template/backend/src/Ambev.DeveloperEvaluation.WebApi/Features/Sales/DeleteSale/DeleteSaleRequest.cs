namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    /// <summary>
    /// Represents a request to delete a sale in the system.
    /// </summary>
    public class DeleteSaleRequest 
    {
        /// <summary>
        /// The unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }
    }
}
