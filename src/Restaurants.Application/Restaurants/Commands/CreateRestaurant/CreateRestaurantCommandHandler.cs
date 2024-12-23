﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper , IUserContext userContext,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser= userContext.GetCurrentUser();
            logger.LogInformation("{UserEmail} [{UserID}] is creating a new restaurant {@Restaurant}",currentUser.Email, currentUser.Id, request);
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId= currentUser.Id;
            if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Create))
                throw new ForbidException();

            int id = await restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}
