using MediatR;


namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommand:IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string UserRole { get; set; } = default!;
    }
}
