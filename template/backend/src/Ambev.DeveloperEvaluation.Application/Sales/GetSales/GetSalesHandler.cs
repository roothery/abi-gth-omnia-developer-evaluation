using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Handler for processing GetSalesCommand requests.
    /// </summary>
    public class GetSalesHandler : IRequestHandler<GetSalesCommand, PaginatedList<GetSalesResult>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetSalesHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public GetSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetSalesCommand request.
        /// </summary>
        /// <param name="request">The get sales command providing optional filters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The retrieved sale list and their items.</returns>
        public async Task<PaginatedList<GetSalesResult>> Handle(GetSalesCommand request, CancellationToken cancellationToken)
        {
            var salePaginated = await _saleRepository.GetListAsync(request.SaleNumber, request.IsCanceled,
                request.Branch, request.Customer, request.StartSaleDate, request.EndSaleDate,
                request.Page, request.PageSize, request.SortBy, request.IsDesc, cancellationToken);

            var salesResult = _mapper.Map<List<GetSalesResult>>(salePaginated.Items);

            var result = new PaginatedList<GetSalesResult>
            {
                Items = salesResult,
                Page = salePaginated.Page,
                PageSize = salePaginated.PageSize,
                TotalCount = salePaginated.TotalCount
            };

            return result;
        }
    }
}
