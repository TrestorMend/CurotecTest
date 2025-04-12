using Application.CQRS.Users.Commands;
using FluentValidation;

namespace Application.CQRS.Users.Validator
{
    public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand>
    {
        public UserDeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
