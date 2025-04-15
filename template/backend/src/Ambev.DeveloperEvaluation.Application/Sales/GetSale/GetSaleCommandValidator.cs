using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Validator for GetSaleCommand.
    /// </summary>
    public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleCommand.
        /// </summary>
        public GetSaleCommandValidator()
        {
            RuleFor(sale => sale.Id).NotEmpty();
        }
    }
}
