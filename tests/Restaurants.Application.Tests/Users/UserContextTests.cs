using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        // TestMethod_Scenario_ExpectedResult
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {

            //Arrange 

            var dateOfBirth = new DateOnly(1999, 1, 1);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,"1"),
                new(ClaimTypes.Email,"test@test.com"),
                new(ClaimTypes.Role,UserRoles.Admin),
                new(ClaimTypes.Role,UserRoles.User),
                new("Nationality","German"),
                new("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd"))
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"test"));
            httpContextAccessorMock.Setup(x=> x.HttpContext).Returns(new DefaultHttpContext()
            {
                User= user,
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);

            //Act

            var currentUser = userContext.GetCurrentUser();

            //Assert

            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Nationality.Should().Be("German");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser.DateOfBirth.Should().Be(dateOfBirth);
        }

        [Fact]  
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            //Arrange 
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            //Act
            Action action= () => userContext.GetCurrentUser();

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage("User context is not present"); 
        }
    }
}