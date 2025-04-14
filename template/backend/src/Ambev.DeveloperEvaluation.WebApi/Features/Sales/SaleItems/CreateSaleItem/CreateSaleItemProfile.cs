using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Profile for mapping between Application and API CreateSaleItem responses.
    /// </summary>
    public class CreateSaleItemProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSaleItem feature.
        /// </summary>
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
        }
    }
}
