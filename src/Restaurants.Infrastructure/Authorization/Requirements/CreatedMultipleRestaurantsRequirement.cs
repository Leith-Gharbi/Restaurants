

using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirement(int minimumRestaurantsOwners) : IAuthorizationRequirement
    {

        public int MinimumRestaurantsOwners { get; } = minimumRestaurantsOwners;
    }
}
