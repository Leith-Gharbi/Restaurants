using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsValidator :AbstractValidator<GetAllRestaurantsQuery>
    {


        private int[] allowPageSizs = [5, 10, 15, 30];
        private string[] allowSortByColumnNames = [nameof(RestaurantDto.Description), nameof(RestaurantDto.Name), nameof(RestaurantDto.Category)];
        public GetAllRestaurantsValidator()
        {
            RuleFor(r=>r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize)
                .Must(value => allowPageSizs.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowPageSizs)}]");

            RuleFor(r => r.SortBy)
               .Must(value => allowSortByColumnNames.Contains(value))
               .When(q=>q.SortBy!=null)
               .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowSortByColumnNames)}]");
        }
    }
}
