using FluentValidation.TestHelper;
using Xunit;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]

        // TestMethode_Senario_ExpectedResult
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Description = "description",
                Category = "Italian",
                ContactEmail = "Test@test.com",
                PostalCode = "12-345",

            };

            var validator = new CreateRestaurantCommandValidator();



            // Act

            var result = validator.TestValidate(command);


            //Assert

            result.ShouldNotHaveAnyValidationErrors();

        }

        [Fact()]

        // TestMethode_Senario_ExpectedResult
        public void Validator_ForValidCommand_ShouldHaveValidationErrors()
        {
            // Arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "Te",
                Description = "description",
                Category = "Tunisien",
                ContactEmail = "@test.com",
                PostalCode = "12345",

            };

            var validator = new CreateRestaurantCommandValidator();

            // Act

            var result = validator.TestValidate(command);

            //Assert

            result.ShouldHaveValidationErrorFor(c=>c.Name);
            result.ShouldHaveValidationErrorFor(c=>c.Category);
            result.ShouldHaveValidationErrorFor(c=>c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c=>c.PostalCode);

        }

        [Theory()]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Indian")]
        // TestMethode_Senario_ExpectedResult
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            // Arrange

            var validator = new CreateRestaurantCommandValidator();

            var command = new CreateRestaurantCommand() { Category = category };

            // Act

            var result = validator.TestValidate(command);

            //Assert

            result.ShouldNotHaveValidationErrorFor(c => c.Category);
      

        }


        [Theory()]
        [InlineData("122-369")]
        [InlineData("1258714")]
   
        // TestMethode_Senario_ExpectedResult
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForpostalCodeProperty(string postalCode)
        {
            // Arrange

            var validator = new CreateRestaurantCommandValidator();

            var command = new CreateRestaurantCommand() { PostalCode = postalCode };

            // Act

            var result = validator.TestValidate(command);

            //Assert

            result.ShouldHaveValidationErrorFor(c => c.PostalCode);


        }
    }
}