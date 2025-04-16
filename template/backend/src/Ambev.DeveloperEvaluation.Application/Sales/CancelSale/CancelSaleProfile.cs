using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and CancelSaleResponse
    /// </summary>
    public class CancelSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CancelSale operation
        /// </summary>
        public CancelSaleProfile()
        {
            CreateMap<CancelSaleCommand, Sale>();
            CreateMap<Sale, CancelSaleResult>();
        }
    }
}
