using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Validator for CancelSaleCommand.
    /// </summary>
    public class CancelSaleCommandValidator: AbstractValidator<CancelSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for CancelSaleCommand.
        /// </summary>
        public CancelSaleCommandValidator()
        {
            RuleFor(sale => sale.Id).NotEmpty();
        }
    }
}
