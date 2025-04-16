using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.UpdateSaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleCommand that defines validation rules for sale update.
    /// </summary>
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - SaleNumber: Required, must be between 5 and 20 characters
        /// - SaleDate: Must be less than or equal to the current date
        /// - Customer: Must be a valid enum value
        /// - Branch: Must be a valid enum value
        /// - Items: At least one item is required
        /// - Items: Each item is validated using UpdateSaleItemCommandValidator
        /// </remarks>
        public UpdateSaleCommandValidator()
        {
            RuleFor(sale => sale.SaleNumber).NotEmpty().Length(5, 20);
            RuleFor(sale => sale.SaleDate).LessThanOrEqualTo(DateTime.Now);
            RuleFor(sale => sale.Customer).IsInEnum();
            RuleFor(sale => sale.Branch).IsInEnum();
            RuleForEach(sale => sale.Items).SetValidator(new UpdateSaleItemCommandValidator());
        }
    }
}
