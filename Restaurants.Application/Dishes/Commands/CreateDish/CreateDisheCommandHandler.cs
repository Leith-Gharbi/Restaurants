using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDisheCommandHandler(ILogger<CreateDisheCommandHandler> logger ,
        IRestaurantsRepository restaurantsRepository , IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService ,
        IDishesRepository dishesRepository) : IRequestHandler<CreateDisheCommand,int>
    {
        public async Task<int> Handle(CreateDisheCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish : {@DishRequest}", request);

            var restaurant = await restaurantsRepository.GetByIdAync(request.RestaurantId);

            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                throw new ForbidException();
            var dish = mapper.Map<Dish>(request);


            return  await dishesRepository.Create(dish);
           
        }
    }
}
