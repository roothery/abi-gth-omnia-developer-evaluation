﻿using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IBus bus)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _bus = bus;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request.
        /// </summary>
        /// <param name="command">The CreateSale command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="validator">The validator for CreateSaleCommand.</param>
        /// <returns>The created sale details.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
            if (existingSale != null)
                throw new InvalidOperationException($"Sale with number {command.SaleNumber} already exists");

            var sale = _mapper.Map<Sale>(command);
            sale.IsCancelled = false;

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            await _bus.Publish(new SaleCreated(createdSale.Id, DateTime.UtcNow));

            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}
