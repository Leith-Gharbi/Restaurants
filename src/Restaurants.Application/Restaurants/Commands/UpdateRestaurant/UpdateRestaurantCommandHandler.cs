using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger ,IRestaurantAuthorizationService restaurantAuthorizationService, IRestaurantsRepository restaurantsRepository , IMapper mapper)
        : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdateRestaurant}",request.Id , request);
            var restaurant = await restaurantsRepository.GetByIdAync(request.Id);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                throw new ForbidException();
            mapper.Map(request, restaurant);
            await restaurantsRepository.SaveChanges();

     
        }
    }
}
