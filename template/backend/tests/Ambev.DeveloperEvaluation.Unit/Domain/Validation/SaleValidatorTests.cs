using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    /// <summary>
    /// Contains unit tests for the SaleValidator class.
    /// Tests cover validation of all sale properties.
    /// </summary>
    public class SaleValidatorTests
    {
        private readonly SaleValidator _validator;

        public SaleValidatorTests()
        {
            _validator = new SaleValidator();
        }

        /// <summary>
        /// Tests that validation passes when all sale properties are valid.
        /// Passes all validation rules without any errors.
        /// </summary>
        [Fact(DisplayName = "Valid sale should pass all validation rules")]
        public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests that validation fails for invalid sale number formats.
        /// This test verifies that usernames that are:
        /// - Empty strings
        /// - Less than 5 characters
        /// Fail validation with appropriate error messages.
        /// The sale number is a required field and must be between 5 and 20 characters.
        /// </summary>
        /// <param name="saleNumber">The invalid sale number to test.</param>
        [Theory(DisplayName = "Validation should fail when sale number is invalid (empty or too short)")]
        [InlineData("")] // Empty
        [InlineData("abcd")] // Less than 5 characters
        public void Given_InvalidSaleNumber_When_Validated_Then_ShouldHaveError(string saleNumber)
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.SaleNumber = saleNumber;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
        }

        /// <summary>
        /// Tests that validation fails when sale number exceeds maximum length.
        /// This test verifies that sale number longer than 20 characters fail validation.
        /// </summary>
        [Theory(DisplayName = "Validation should fail when sale number exceeds maximum length")]
        [InlineData("greaterthan20characters")] // Greater than 20 characters
        public void Given_SaleNumberLongerThanMaximum_When_Validated_Then_ShouldHaveError(string saleNumber)
        {
            var sale = SaleTestData.GenerateValidSale();
            sale.SaleNumber = saleNumber;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
        }

        /// <summary>
        /// Tests that validation fails when the sale date is set in the future.
        /// </summary>
        [Fact(DisplayName = "Validation should fail when sale date is in the future")]
        public void Given_SaleWithFutureDate_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.SaleDate = DateTime.Now.AddDays(1);

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.SaleDate);
        }

        /// <summary>
        /// Tests that validation fails when the customer is invalid.
        /// </summary>
        [Fact(DisplayName = "Validation should fail when the customer is invalid")]
        public void Given_InvalidCustomer_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Customer = (Customer)999;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.Customer);
        }

        /// <summary>
        /// Tests that validation fails when the branch is invalid.
        /// </summary>
        [Fact(DisplayName = "Validation should fail when the branch is invalid")]
        public void Given_InvalidBranch_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Branch = (Branch)999;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.Branch);
        }

        /// <summary>
        /// Tests that validation failw when the sale has no items.
        /// </summary>
        [Fact(DisplayName = "Validation should fail when items is empty")]
        public void Given_EmptyItemsList_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Items.Clear();

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.Items);
        }

        /// <summary>
        /// Tests that validation fails when total amount is 0 and there are non-cancelled items.
        /// </summary>
        [Fact(DisplayName = "Validation should fail when total amount is 0 and there are non-cancelled items")]
        public void Given_ZeroTotalAmountWithValidItems_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale(clearListItem: true);
            sale.Items = SaleTestData.GenerateInvalidItemsWhitZeroTotalAmount(sale.Id, 3);

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.TotalAmount);
        }

        /// <summary>
        /// Tests that validation fails when quantity is 0 or greater than 20.
        /// </summary>
        [Theory(DisplayName = "Validation should fail when quantity is 0 or exceeds 20")]
        [InlineData(0)]
        [InlineData(21)]
        public void Given_InvalidQuantity_When_Validated_Then_ShouldHaveError(int quantity)
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Items[0].Quantity = quantity;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor("Items[0].Quantity");
        }
    }
}
