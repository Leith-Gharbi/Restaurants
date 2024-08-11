using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
    {

        private readonly List<String> validCategories = ["Italien", "Mexican", "Japanes", "American", "Indain"];
        public CreateRestaurantDtoValidator()
        {
            RuleFor(dto => dto.Name).Length(3, 100);
            RuleFor(dto => dto.Description).NotEmpty().WithMessage(x=> $"{x.Description} is required.");
            RuleFor(dto => dto.Category)
                .Must(validCategories.Contains)
                .WithMessage($"Invalid category. Please choose from the valid categories ");
            //    .Custom((value, context) => {
            //    var isvalidCategory = validCategories.Contains(value);
            //    if (!isvalidCategory)
            //    {
            //        context.AddFailure("Category", "Invalid category. Please choose from the valid categories ");
            //    }
            //});
            RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage($"Please provide a valid email address");
            RuleFor(dto => dto.PostalCode).Matches(@"^\d{2}-\d{3}$").WithMessage($"Please provide a valid postal code  (XX-XXX)");

          
        }
    }
}
