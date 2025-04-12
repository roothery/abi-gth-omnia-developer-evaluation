using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a customer sale including items, customer and branch information,
    /// as well as cancellation status and validation logic
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets the unique identifier for the sale
        /// </summary>
        public string SaleNumber { get; private set; } = String.Empty;

        /// <summary>
        /// Gets the date the sale was created
        /// </summary>
        public DateTime SaleDate { get; private set; }

        /// <summary>
        /// Gets the customer associated with the sale
        /// </summary>
        public Customer Customer { get; private set; }

        /// <summary>
        /// Gets the branch where the sale was made
        /// </summary>
        public Branch Branch { get; private set; }

        /// <summary>
        /// Gets the list of items included in the sale
        /// </summary>
        public List<SaleItem> Items { get; private set; } = new();

        /// <summary>
        /// Gets a value indicating whether the sale was cancelled
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the total monetary amount of the sale,
        /// excluding items if the sale is cancelled
        /// </summary>
        public decimal TotalAmount => IsCancelled ? 0 : Items.Sum(i => i.TotalAmount);

        /// <summary>
        /// Initializes a new instance of the sale,
        /// setting the sale date to the current time and validating the instance
        /// </summary>
        public Sale()
        {
            SaleDate = DateTime.Now;
            Validate();
        }

        /// <summary>
        /// Performs validation of the sale entity using the SaleValidator rules
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail" /> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        /// <remarks>
        /// <listheader>The validation includes checking:</listheader>
        /// <list type="bullet">Sale number format and uniqueness</list>
        /// <list type="bullet">Sale date validity</list>
        /// <list type="bullet">Customer and branch verification</list>
        /// <list type="bullet">Items validity</list>
        /// <list type="bullet">Total amount validity</list>
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Cancels the sale
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}
