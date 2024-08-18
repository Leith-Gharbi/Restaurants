using FluentValidation;


namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDisheCommandValidator : AbstractValidator<CreateDisheCommand>
    {
        public CreateDisheCommandValidator() {
        
            RuleFor(dto => dto.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative number.");
            RuleFor(dto => dto.KiloCalories).GreaterThanOrEqualTo(0).WithMessage("KiloCalories must be a non-negative number.");
        }
    }
}
