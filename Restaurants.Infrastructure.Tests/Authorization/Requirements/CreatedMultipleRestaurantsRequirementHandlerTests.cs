using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;


namespace Restaurants.Infrastructure.Authorization.Requirements.Tests;

public class CreatedMultipleRestaurantsRequirementHandlerTests
{
    [Fact()]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
    {

        //Arrange 
        var userContextMock = new  Mock<IUserContext>();

        var currentUser = new CurrentUser("1", "test@test.com", [],null,null);

        var loggerMock = new Mock<ILogger<CreatedMultipleRestaurantsRequirementHandler>>();

        userContextMock.Setup(m=>m.GetCurrentUser()).Returns(currentUser);


        var restaurants = new List<Restaurant>()
        {
            new ()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = currentUser.Id,
            }
        };


        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();

        restaurantRepositoryMock.Setup(r => r.GetByOwnerIdAsync(currentUser.Id)).ReturnsAsync(restaurants);


        var requirement = new CreatedMultipleRestaurantsRequirement(1);


        var handler = new CreatedMultipleRestaurantsRequirementHandler(loggerMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);
      
        
       var context = new AuthorizationHandlerContext([requirement],null,null);
        
        //Acte 

        await handler.HandleAsync(context);



        //Assert

        context.HasSucceeded.Should().BeTrue();

    }
    [Fact()]
    public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
    {

        //Arrange 
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);

        var userContextMock = new Mock<IUserContext>();

        var loggerMock = new Mock<ILogger<CreatedMultipleRestaurantsRequirementHandler>>();

        userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);


        var restaurants = new List<Restaurant>()
            {

                new()
                {
                    OwnerId = currentUser.Id,
                },
                  
            };


        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();

        restaurantRepositoryMock.Setup(r => r.GetByOwnerIdAsync(currentUser.Id)).ReturnsAsync(restaurants);


        var requirement = new CreatedMultipleRestaurantsRequirement(2);


        var handler = new CreatedMultipleRestaurantsRequirementHandler(loggerMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);


        var context = new AuthorizationHandlerContext([requirement], null, null);

        //Acte 

        await handler.HandleAsync(context);



        //Assert

        context.HasFailed.Should().BeTrue();

    }


}