using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class UpdateSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid Sale entities.
        /// </summary>
        private static readonly Faker<UpdateSaleCommand> updateSaleHandlerFaker = new Faker<UpdateSaleCommand>()
            .RuleFor(s => s.Id, f => f.Random.Guid())
            .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(f.Random.Int(5, 20)))
            .RuleFor(s => s.SaleDate, f => f.Date.Past())
            .RuleFor(s => s.Customer, f => f.PickRandom<Customer>())
            .RuleFor(s => s.Branch, f => f.PickRandom<Branch>())
            .RuleFor(s => s.Items, (f, s) => BuildUpdateSaleItemCommand(s));

        /// <summary>
        /// Generates a list of valid UpdateSaleItemCommand entries using Faker.
        /// Each generated item includes:
        /// - Id (random GUID)
        /// - SaleId (copied from the provided UpdateSaleCommand)
        /// - Product (random valid Product value)
        /// - Quantity (integer between 1 and 20)
        /// - UnitPrice (decimal between 0 and 100)
        /// - IsCancelled (always set to false)
        /// </summary>
        private static List<UpdateSaleItemCommand> BuildUpdateSaleItemCommand(UpdateSaleCommand updateSale)
        {
            var saleItemFaker = new Faker<UpdateSaleItemCommand>()
                .RuleFor(i => i.Id, f => f.Random.Guid())
                .RuleFor(i => i.SaleId, _=> updateSale.Id)
                .RuleFor(i => i.Product, f => f.PickRandom<Product>())
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20))
                .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(0m, 100m))
                .RuleFor(i => i.IsCancelled, _=> false);

            return saleItemFaker.Generate(3);
        }

        /// <summary>
        /// Generates a valid UpdateSaleCommand using Faker.
        /// </summary>
        public static UpdateSaleCommand GenerateValidCommand()
        {
            return updateSaleHandlerFaker.Generate();
        }
    }
}
