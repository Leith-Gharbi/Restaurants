﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurantLogo;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;


namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]   // This for specifing the return type in Swagger Doc
       // [Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantsQuery query )
        {
            var restaurants = await mediator.Send(query);
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)  // ActionResult<RestaurantDto?>  will specify the resonse type in swagger doc
        {

            
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurnt([FromRoute] int id)
        {

            await mediator.Send(new DeleteRestaurantCommand(id));
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {

            int id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, null);



        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);

            return NoContent();

        }



        [HttpPost("{id}/logo")]
        public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var command = new UploadRestaurantLogoCommand()
            {
                RestaurantId = id,
                FileName = $"{id}-{file.FileName}",
                File = stream
            };

            await mediator.Send(command);
            return NoContent();
        }


    }
}
