using FluentValidation;
namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsValidator :AbstractValidator<GetAllRestaurantsQuery>
    {


        private int[] allowPageSizs = [5, 10, 15, 30];
        public GetAllRestaurantsValidator()
        {
            RuleFor(r=>r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize)
                .Must(value => allowPageSizs.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowPageSizs)}]");
        }
    }
}
