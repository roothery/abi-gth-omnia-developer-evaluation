using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the Sale entity class.
    /// Tests cover status changes and validation scenarios.
    /// </summary>
    public class SaleTests
    {
        /// <summary>
        /// Validates that a valid sale is marked as valid.
        /// </summary>
        [Fact(DisplayName = "Should return valid result for a valid sale")]
        public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = sale.Validate();

            // Assert
            result.IsValid.Should().BeTrue();
        }

        /// <summary>
        /// Ensures no validation errors are returned for a valid sale.
        /// </summary>
        [Fact(DisplayName = "Should not return validation errors for a valid sale")]
        public void Given_ValidSaleData_When_Validated_Then_ShouldReturnNoErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = sale.Validate();

            // Assert
            result.Errors.Should().BeEmpty();
        }

        /// <summary>
        /// Marks the sale as cancelled when Cancel is called.
        /// </summary>
        [Fact(DisplayName = "Sale should be marked as Cancelled when Cancel() is called")]
        public void Given_ActiveSale_When_Cancelled_Then_StatusIsCancelled()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.Cancel();

            // Assert
            sale.IsCancelled.Should().BeTrue();
        }

        /// <summary>
        /// Throws an exception when item quantity exceeds the allowed limit.
        /// </summary>
        [Fact(DisplayName = "Creating more than the allowed number of identical items should throw an exception")]
        public void Given_ItemQuantityExceedsLimit_When_Created_Then_ShouldThrowException()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var product = Product.ProductA;
            var quantityExceeded = 21;
            var unitPrice = 100m;

            // Act
            Action action = () => new SaleItem(saleId, product, quantityExceeded, unitPrice);

            //Assert
            Assert.Throws<InvalidOperationException>(action);
        }

        /// <summary>
        /// Applies discount based on item quantity.
        /// </summary>
        /// <param name="quantity">Item quantity.</param>
        /// <param name="discountRate">Discount rate.</param>
        [Theory(DisplayName = "Discount should be applied based on quantity of items")]
        [InlineData(3, 0)]
        [InlineData(4, 0.1)]
        [InlineData(10, 0.2)]
        public void Given_ItemQuantity_When_Created_Then_DiscountShouldBeAppliedCorrectly(int quantity, decimal discountRate)
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale(clearListItem: true);
            var product = Product.ProductA;
            var unitPrice = 100m;

            // Act
            var saleItem = new SaleItem(sale.Id, product, quantity, unitPrice);
            sale.Items.Add(saleItem);

            // Assert
            var expectedDiscount = quantity * unitPrice * discountRate;
            saleItem.Discount.Should().Be(expectedDiscount);
        }
    }
}
