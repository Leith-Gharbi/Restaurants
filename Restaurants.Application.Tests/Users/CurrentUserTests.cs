using Restaurants.Domain.Constants;
using FluentAssertions;
using Xunit;


namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {

        // TestMethod_Scenario_ExpectedResult
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            // Arrange : Set up any objects, variables, or prerequisites needed for the test

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            // Act :Execute the unit being tested (usually a method).

            var isInRole = currentUser.IsInRole(roleName);

            // Assert : Verify that the expected result matches the actual result.

            isInRole.Should().BeTrue();

        }


        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            // Arrange : Set up any objects, variables, or prerequisites needed for the test

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            // Act :Execute the unit being tested (usually a method).

            var isInRole = currentUser.IsInRole(UserRoles.Owner);

            // Assert : Verify that the expected result matches the actual result.

            isInRole.Should().BeFalse();

        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            // Arrange : Set up any objects, variables, or prerequisites needed for the test

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            // Act :Execute the unit being tested (usually a method).

            var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

            // Assert : Verify that the expected result matches the actual result.

            isInRole.Should().BeFalse();

        }
    }
}