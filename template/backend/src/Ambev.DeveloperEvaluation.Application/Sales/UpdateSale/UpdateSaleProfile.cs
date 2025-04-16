using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and UpdateSaleResponse
    /// </summary>
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Sale>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                .ForMember(s => s.Items, opt => opt.Ignore());
            CreateMap<Sale, UpdateSaleResult>();
        }
    }
}
