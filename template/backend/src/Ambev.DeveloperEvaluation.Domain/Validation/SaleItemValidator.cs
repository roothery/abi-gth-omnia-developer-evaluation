using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(item => item.Product)
                .IsInEnum().WithMessage("Product is invalid.");

            RuleFor(item => item.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Item quantity must be greater than or equal to 0.");

            RuleFor(item => item.Quantity)
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 items of the same product.");

            RuleFor(item => item.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to 0.");

            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be zero or a positive value.");

            RuleFor(item => item.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .When(item => !item.IsCancelled)
                .WithMessage("Total amount must be greater than 0 for non-cancelled items.");
        }
    }
}
