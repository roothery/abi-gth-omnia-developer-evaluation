using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides test data for the Sale entity using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class SaleTestData
    {
        /// <summary>
        /// Configures a Faker to generate valid instances of the Sale entity.
        /// Each generated Sale includes:
        /// - A unique ID and sale number
        /// - A valid past sale date
        /// - A random customer and branch
        /// - A list of valid sale item
        /// - A cancellation status (true/false)
        /// - A total amount calculated from item totals
        /// </summary>
        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
            .CustomInstantiator(f => new Sale())
            .RuleFor(s => s.Id, f => f.Random.Guid())
            .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(f.Random.Int(5, 20)))
            .RuleFor(s => s.SaleDate, f => f.Date.Past())
            .RuleFor(s => s.Customer, f => f.PickRandom<Customer>())
            .RuleFor(s => s.Branch, f => f.PickRandom<Branch>())
            .RuleFor(s => s.Items, (f, s) => BuildSaleItems(s.Id))
            .RuleFor(s => s.IsCancelled, f => f.Random.Bool())
            .RuleFor(s => s.TotalAmount, (f, s) => s.Items.Sum(i => i.TotalAmount));

        /// <summary>
        /// Builds a list of sale item instances for a given Sale ID.
        /// </summary>
        /// <param name="saleId">The ID of the sale to associate the items with.</param>
        /// <returns>List of sale item instances.</returns>
        private static List<SaleItem> BuildSaleItems(Guid saleId)
        {
            var saleItemFaker = new Faker<SaleItem>()
                .CustomInstantiator(f => new SaleItem(
                    saleId,
                    f.PickRandom<Product>(),
                    f.Random.Int(1, 20),
                    f.Random.Decimal(0m, 100m),
                    f.Random.Bool()
                ));

            return saleItemFaker.Generate(4);
        }

        /// <summary>
        /// Generates a valid Sale instance with randomized data.
        /// Optionally clears the list of sale item.
        /// </summary>
        /// <param name="clearListItem">If true, clears the generated sale's item list.</param>
        /// <returns>A valid Sale instance.</returns>
        public static Sale GenerateValidSale(bool clearListItem = false)
        {
            var saleFaker = SaleFaker.Generate();

            if (clearListItem)
                saleFaker.Items.Clear();

            return saleFaker;
        }

        /// <summary>
        /// Generates a list of sale item instances with a calculated TotalAmount equal to zero.
        /// Each item is created with a quantity of 1, unit price of 0, and not cancelled.
        /// </summary>
        /// <param name="saleId">The ID of the sale to associate with the generated items.</param>
        /// <param name="quantity">The number of items to generate.</param>
        /// <returns>A list of sale item instances with zero total amount.</returns>
        public static List<SaleItem> GenerateInvalidItemsWhitZeroTotalAmount(Guid saleId, int quantity)
        {
            var saleItemFaker = new Faker<SaleItem>()
                .CustomInstantiator(f => new SaleItem(
                    saleId,
                    f.PickRandom<Product>(),
                    1,
                    0,
                    false
                ));

            return saleItemFaker.Generate(quantity);
        }
    }
}
