namespace Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Represents the response returned after successfully creating a new sale item.
    /// </summary>
    /// <remarks>
    /// This response contains the unique identifier of the newly created sale item,
    /// which can be used for subsequent operations or reference.
    /// </remarks>
    public class CreateSaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the newly created sale item.
        /// </summary>
        /// <value>A GUID that uniquely identifies the created sale item in the system.</value>
        public Guid Id { get; set; }
    }
}
