using FluentValidation;


namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommandValidator: AbstractValidator<AssignUserRoleCommand>
    {

        public AssignUserRoleCommandValidator()
        {

            RuleFor(dto => dto.UserEmail).EmailAddress().WithMessage($"Please provide a valid user email address");

        }
    }
}
