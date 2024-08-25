﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Queries.GetDisheByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurant/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDisheCommand command)
        {

            command.RestaurantId = restaurantId;
            await mediator.Send(command);
            return Created();

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>>  GetAllForRestaurant([FromRoute] int restaurantId)
        {

           var dishes=  await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId , [FromRoute]  int dishId)
        {

            var dish = await mediator.Send(new GetDisheByIdForRestaurantQuery(restaurantId , dishId));

            return Ok(dish);

        }
    }
}