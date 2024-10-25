
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    internal class CreatedMultipleRestaurantsRequirementHandler(ILogger<CreatedMultipleRestaurantsRequirementHandler> logger,IRestaurantsRepository restaurantsRepository, IUserContext userContext) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
        {

            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation($"{currentUser!.Email} with id [{currentUser!.Id}] - Handling MinimumRestaurantOwnerRequirement");

            var restaurants =await restaurantsRepository.GetByOwnerIdAsync(currentUser!.Id);

            if (restaurants.Count() < requirement.MinimumRestaurantsOwners)
            {
                logger.LogWarning($"User has not at least {requirement.MinimumRestaurantsOwners} restaurants" );
                context.Fail();

                           }

            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
         
        }
    }
}
