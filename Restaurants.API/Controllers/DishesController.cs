﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;

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
    }
}
