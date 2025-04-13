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
                .GreaterThan(0)
                .LessThanOrEqualTo(20)
                .WithMessage("Item quantity must be greater than 0 and less than or equal to 20.");

            RuleFor(item => item.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to 0.");

            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be greater than or equal to 0.");

            RuleFor(item => item.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");
        }
    }
}
