using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Profile for mapping between Sale entity and GetSalesResponse
    /// </summary>
    public class GetSalesProfile : Profile
    {
        public GetSalesProfile()
        {
            CreateMap<Sale, GetSalesResult>();
            CreateMap<PaginatedList<Sale>, GetSalesResult>();
            CreateMap<SaleItem, GetSaleItemResult>();
        }
    }
}
