using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class CreateSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid Sale entities.
        /// The generated sales will have valid:
        /// - SaleNumber (random string between 5 and 20 characters)
        /// - SaleDate (a past date)
        /// - Customer (random valid Customer value)
        /// - Branch (random valid Branch value)
        /// - Items (a list of valid sale items)
        /// </summary>
        private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(f.Random.Int(5, 20)))
        .RuleFor(s => s.SaleDate, f => f.Date.Past())
        .RuleFor(s => s.Customer, f => f.PickRandom<Customer>())
        .RuleFor(s => s.Branch, f => f.PickRandom<Branch>())
        .RuleFor(s => s.Items, f => BuildSaleItemsCommand());

        /// <summary>
        /// Generates a list of valid SaleItem entities using Faker.
        /// Each generated item includes:
        /// - Product (random valid Product value)
        /// - Quantity (integer between 1 and 20)
        /// - UnitPrice (decimal between 0 and 100)
        /// </summary>
        private static List<CreateSaleItemCommand> BuildSaleItemsCommand()
        {
            var saleItemFaker = new Faker<CreateSaleItemCommand>()
                .RuleFor(i => i.Product, f => f.PickRandom<Product>())
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20))
                .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(0m, 100m));

            return saleItemFaker.Generate(4);
        }

        /// <summary>
        /// Generates a valid Sale entity with randomized data.
        /// The generated sale will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid Sale entity with randomly generated data.</returns>
        public static CreateSaleCommand GenerateValidCommand()
        {
            return createSaleHandlerFaker.Generate();
        }
    }
}
