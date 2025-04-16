using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleItems.UpdateSaleItem
{
    // <summary>
    /// Validator for UpdateSaleItemCommandValidator that defines validation rules for sale item command.
    /// </summary>
    public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSaleItemCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Product: Must be a valid enum value
        /// - Quantity: Must be greater than 0 and less than or equal to 20
        /// - UnitPrice: Must be greater than or equal to 0
        /// </remarks>
        public UpdateSaleItemCommandValidator()
        {
            RuleFor(item => item.Product).IsInEnum();
            RuleFor(item => item.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(item => item.UnitPrice).GreaterThanOrEqualTo(0);
        }
    }
}
