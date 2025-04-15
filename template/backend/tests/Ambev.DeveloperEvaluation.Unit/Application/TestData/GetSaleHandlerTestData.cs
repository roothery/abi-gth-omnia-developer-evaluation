using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class GetSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid Sale entities.
        /// </summary>
        private static readonly Faker<GetSaleCommand> getSaleHandlerFaker = new Faker<GetSaleCommand>()
            .RuleFor(c => c.Id, f => f.Random.Guid());

        /// <summary>
        /// Generates a valid Sale with randomized data.
        /// </summary>
        /// <returns>A valid Sale entity with a randomly generated SaleId.</returns>
        public static GetSaleCommand GenerateValidCommand()
        {
            return getSaleHandlerFaker.Generate();
        }

        /// <summary>
        /// Generates a fake Sale with realistic data for testing retrieval,
        /// useful for mocking repository returns.
        /// </summary>
        /// <returns>A Sale with valid random data.</returns>
        public static Sale GenerateValidFakeSale()
        {
            var saleFaker = new Faker<Sale>()
                .RuleFor(s => s.Id, f => f.Random.Guid())
                .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(f.Random.Int(5, 20)))
                .RuleFor(s => s.SaleDate, f => f.Date.Past())
                .RuleFor(s => s.Customer, f => f.PickRandom<Customer>())
                .RuleFor(s => s.Branch, f => f.PickRandom<Branch>())
                .RuleFor(s => s.Items, (f, s) =>
                {
                    var itemFaker = new Faker<SaleItem>()
                        .CustomInstantiator(f => new SaleItem(
                            s.Id,
                            f.PickRandom<Product>(),
                            f.Random.Int(1, 10),
                            f.Random.Decimal(0m, 100m)
                        ));
                    return itemFaker.Generate(3);
                });

            return saleFaker.Generate();
        }

        public static GetSaleResult CreateSaleResult(Sale sale)
        {
            return new GetSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                Items = sale.Items.Select(i => new GetSaleItemResult
                {
                    Id = i.Id,
                    SaleId = i.SaleId,
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    IsCancelled = i.IsCancelled,
                    TotalAmount = i.TotalAmount
                }).ToList(),
                IsCancelled = sale.IsCancelled,
                TotalAmount = sale.TotalAmount,
            };
        }
    }
}
