using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleCommand that defines validation rules for sale creation command.
    /// </summary>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - SaleNumber: Required, must be between 5 and 20 characters
        /// - SaleDate: Must be less than or equal to the current date
        /// - Customer: Must be a valid enum value
        /// - Branch: Must be a valid enum value
        /// - Items: At least one item is required
        /// - Items: Each item is validated using CreateSaleItemCommandValidator
        /// </remarks>
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.SaleNumber).NotEmpty().Length(5, 20);
            RuleFor(sale => sale.SaleDate).LessThanOrEqualTo(DateTime.Now);
            RuleFor(sale => sale.Customer).IsInEnum();
            RuleFor(sale => sale.Branch).IsInEnum();
            RuleFor(sale => sale.Items).NotEmpty();
            RuleForEach(sale => sale.Items).SetValidator(new CreateSaleItemCommandValidator());
        }
    }
}