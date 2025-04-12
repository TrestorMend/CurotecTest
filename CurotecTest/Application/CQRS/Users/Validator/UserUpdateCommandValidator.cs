using Application.CQRS.Users.Commands;
using FluentValidation;

namespace Application.CQRS.Users.Validator
{
    public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
    {
        public UserUpdateCommandValidator()
        {
            Include(new BaseUserCommandValidator());
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
