

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDisheByIdForRestaurant
{
    public class GetDisheByIdForRestaurantQueryHandler(ILogger<GetDisheByIdForRestaurantQueryHandler> logger , IRestaurantsRepository restaurantsRepository ,IMapper mapper) : IRequestHandler<GetDisheByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDisheByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish : {DishId}, for restaurant with id: {RestaurantId}", request.RestaurantId, request.DishId);
            var restaurant = await restaurantsRepository.GetByIdAync(request.RestaurantId);
            if(restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dish = restaurant.Dishes.FirstOrDefault(x=>x.Id==request.DishId);
            if(dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            var result = mapper.Map<DishDto>(dish);
            return result;           
        }
    }
}
