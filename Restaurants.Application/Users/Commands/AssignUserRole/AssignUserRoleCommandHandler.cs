using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;


namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger , UserManager<User> userManager , RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Assigning user role : {@Request }", request);

            var user = await userManager.FindByEmailAsync(request.UserEmail);

            if (user == null) throw new NotFoundException(nameof(User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.UserRole);

            if(role == null) throw new NotFoundException(nameof(IdentityRole), request.UserRole);

             await userManager.AddToRoleAsync(user, role.Name!);

            
        }
    }
}
