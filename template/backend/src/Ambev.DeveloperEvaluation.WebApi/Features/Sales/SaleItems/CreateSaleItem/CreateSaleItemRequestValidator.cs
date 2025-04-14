using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Validator for CreateSaleItemRequest that defines validation rules for sale creation.
    /// </summary>
    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        /// <summary>
        /// Validates instances of the CreateSaleItemRequest.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Product: Must be a valid enum value.
        /// - Quantity: Must be greater than 0 and less than or equal to 20.
        /// - UnitPrice: Must be greater than or equal to 0.
        /// </remarks>
        public CreateSaleItemRequestValidator()
        {
            RuleFor(item => item.Product).IsInEnum();
            RuleFor(item => item.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(item => item.UnitPrice).GreaterThanOrEqualTo(0);
        }
    }
}
