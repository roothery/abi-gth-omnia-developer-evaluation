namespace Ambev.DeveloperEvaluation.Application.Events
{
    public record SaleDeleted(Guid SaleId, DateTime DeletedAt);
}
