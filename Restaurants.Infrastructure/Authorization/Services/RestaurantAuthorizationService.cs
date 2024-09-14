using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant resataurant, ResourceOperation resourceOperation)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}", currentUser.Email, resourceOperation, resataurant.Name);

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }
            if (resourceOperation == ResourceOperation.Delete && currentUser.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if ((resourceOperation == ResourceOperation.Update || resourceOperation == ResourceOperation.Delete) && resataurant.OwnerId == currentUser.Id)
            {
                logger.LogInformation("Resataurant owner - successful authorization");
                return true;
            }
            return false;

        }
    }
}
