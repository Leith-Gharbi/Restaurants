

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnassignUserRole
{
    public class UnassignUserRoleCommandHandler(ILogger<UnassignUserRoleCommandHandler> logger , UserManager<User> userManager , RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
    {
        public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Unassigning user role: {@Request}", request);

            var user = await userManager.FindByEmailAsync(request.UserEmail);

            if (user == null)  throw new NotFoundException(nameof(user),request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            var userRoles = await userManager.GetRolesAsync(user);

            if(!userRoles.Contains(role.Name)) throw new NotFoundException(nameof(userRoles), role.Name);

            await userManager.RemoveFromRoleAsync(user, role.Name);




        }
    }
}
