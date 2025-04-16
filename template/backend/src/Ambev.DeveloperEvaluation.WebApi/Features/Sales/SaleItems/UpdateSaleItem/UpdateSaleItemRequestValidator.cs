using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SaleItems.UpdateSaleItem
{
    /// <summary>
    /// Validator for UpdateSaleItemRequest that defines validation rules for sale update.
    /// </summary>
    public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
    {
        /// <summary>
        /// Validates instances of the UpdateSaleItemRequest.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Product: Must be a valid enum value.
        /// - Quantity: Must be greater than 0 and less than or equal to 20.
        /// - UnitPrice: Must be greater than or equal to 0.
        /// </remarks>
        public UpdateSaleItemRequestValidator()
        {
            RuleFor(item => item.Product).IsInEnum();
            RuleFor(item => item.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(item => item.UnitPrice).GreaterThanOrEqualTo(0);
        }
    }
}
