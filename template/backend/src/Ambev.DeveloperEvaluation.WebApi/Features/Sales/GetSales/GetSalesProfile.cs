using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.GetSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    /// <summary>
    /// Profile for mapping GetSales feature requests to commands.
    /// </summary>
    public class GetSalesProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetSales feature.
        /// </summary>
        public GetSalesProfile()
        {
            CreateMap<GetSalesRequest, GetSalesCommand>();
            CreateMap<GetSalesResult, GetSalesResponse>();
            CreateMap<GetSaleItemResult, GetSaleItemResult>();
        }
    }
}
