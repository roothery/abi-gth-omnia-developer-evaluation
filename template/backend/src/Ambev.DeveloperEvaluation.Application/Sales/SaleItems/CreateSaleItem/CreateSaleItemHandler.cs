using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Handler for processing CreateSaleItemCommand requests
    /// </summary>
    public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateSaleItemHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="validator">The validator for CreateSaleItemCommand.</param>
        public CreateSaleItemHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateSaleItemCommand request.
        /// </summary>
        /// <param name="command">The CreateSaleItem command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale details.</returns>
        public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleItemCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
            if (sale == null)
                throw new InvalidOperationException($"Sale with ID {command.SaleId} not found");

            var saleItem = _mapper.Map<SaleItem>(command);
            sale.Items.Add(saleItem);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var result = _mapper.Map<CreateSaleItemResult>(saleItem);
            return result;
        }
    }
}
