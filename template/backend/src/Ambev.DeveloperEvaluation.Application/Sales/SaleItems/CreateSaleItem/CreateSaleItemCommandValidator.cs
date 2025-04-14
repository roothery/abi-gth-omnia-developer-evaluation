using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Validator for CreateSaleItemCommand that defines validation rules for sale creation command.
    /// </summary>
    public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleItemCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Product: Must be a valid enum value
        /// - Quantity: Must be greater than 0 and less than or equal to 20
        /// - UnitPrice: Must be greater than or equal to 0
        /// </remarks>
        public CreateSaleItemCommandValidator()
        {
            RuleFor(item => item.Product).IsInEnum();
            RuleFor(item => item.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(item => item.UnitPrice).GreaterThanOrEqualTo(0);
        }
    }
}
