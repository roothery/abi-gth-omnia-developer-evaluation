﻿using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an individual item within a sale, including product, pricing,
    /// quantity, discount logic, and cancellation status
    /// </summary>
    public class SaleItem
    {
        /// <summary>
        /// Gets the identifier of the associated sale
        /// </summary>
        public Guid SaleId { get; private set; }

        /// <summary>
        /// Gets the product associated with the sale item
        /// </summary>
        public Product Product{ get; private set; }

        /// <summary>
        /// Gets the quantity of the product purchased
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the unit price of the product
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the discount applied to this item based on quantity rules
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the item was cancelled
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the total amount for the item after applying discount and cancellation logic
        /// </summary>
        public decimal TotalAmount => IsCancelled ? 0 : (UnitPrice * Quantity) - Discount;

        /// <summary>
        /// Initializes a new instance of the sale item with required values and business rules.
        /// </summary>
        public SaleItem(Guid saleId, Product product, int quantity, decimal unitPrice, bool isCancelled = false)
        {
            SaleId = saleId;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity);
            IsCancelled = isCancelled;
            IsMoreThan20Items();
        }

        /// <summary>
        /// Calculates discount based on the quantity of items purchased
        /// </summary>
        private decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 10 && quantity <= 20)
                return 0.20m;
            if (quantity >= 4)
                return 0.10m;
            return 0m;
        }

        /// <summary>
        /// Validates that the quantity of items does not exceed 20
        /// </summary>
        public void IsMoreThan20Items()
        {
            if (Quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 items of the same product.");
        }
    }
}
