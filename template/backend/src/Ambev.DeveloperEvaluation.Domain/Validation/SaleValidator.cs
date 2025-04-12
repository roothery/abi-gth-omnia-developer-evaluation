using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required.")
                .MinimumLength(5).WithMessage("Sale number must be at least 5 characters long.")
                .MaximumLength(20).WithMessage("Sale number cannot be longer than 20 characters.");

            RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Sale date must be less than or equal to the current date.");

            RuleFor(sale => sale.Customer)
                .IsInEnum().WithMessage("Customer is invalid");

            RuleFor(sale => sale.Branch)
                .IsInEnum().WithMessage("Branch is invalid");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("Sale must contain at least one item.");

            RuleFor(sale => sale.TotalAmount)
                .GreaterThan(0)
                .When(sale => !sale.IsCancelled)
                .WithMessage("Total amount must be greater than 0 if the sale is not cancelled.");

            RuleForEach(sale => sale.Items).SetValidator(new SaleItemValidator());
        }
    }
}
