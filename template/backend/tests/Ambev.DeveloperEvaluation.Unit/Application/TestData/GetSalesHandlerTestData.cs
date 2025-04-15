using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
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
    public static class GetSalesHandlerTestData
    {
        /// <summary>
        /// Generates a fake Sale with realistic data for testing retrieval,
        /// useful for mocking repository returns.
        /// </summary>
        /// <returns>A Sale with valid random data.</returns>
        public static List<Sale> GenerateValidFakeSales(int quantity = 15)
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

            return saleFaker.Generate(quantity);
        }

        /// <summary>
        /// Converts a list of Sale entities into a list of GetListSaleResult,
        /// mapping all relevant properties including items.
        /// </summary>
        /// <param name="sales">List of sales to convert.</param>
        /// <returns>List of mapped GetListSaleResult objects.</returns>
        public static List<GetSalesResult> GenerateGetSalesResults(List<Sale> sales)
        {
            return sales.Select(sale => new GetSalesResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                IsCancelled = sale.IsCancelled,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(item => new GetSaleItemResult
                {
                    Id = item.Id,
                    SaleId = item.SaleId,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    IsCancelled = item.IsCancelled,
                    TotalAmount = item.TotalAmount
                }).ToList()
            }).ToList();
        }
    }
}
