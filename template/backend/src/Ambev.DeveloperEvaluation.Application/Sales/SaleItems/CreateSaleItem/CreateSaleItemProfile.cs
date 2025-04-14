using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Profile for mapping between SaleItem entity and CreateSaleItemResponse
    /// </summary>
    public class CreateSaleItemProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSaleItem operation
        /// </summary>
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemCommand, SaleItem>()
            .ConstructUsing(cmd => new SaleItem(
                cmd.SaleId,
                cmd.Product,
                cmd.Quantity,
                cmd.UnitPrice,
                cmd.IsCancelled));
            CreateMap<SaleItem, CreateSaleItemResult>();

        }
    }
}
