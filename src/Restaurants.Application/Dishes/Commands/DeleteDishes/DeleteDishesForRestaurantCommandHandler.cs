

using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger ,IRestaurantAuthorizationService restaurantAuthorizationService, IRestaurantsRepository restaurantsRepository , IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
           logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAync(request.RestaurantId);

            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());


            if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                throw new ForbidException();

            await  dishesRepository.Delete(restaurant.Dishes);


        }
    }
}
