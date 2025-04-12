using Application.CQRS.Users.Commands;
using FluentValidation;

namespace Application.CQRS.Users.Validator
{
    public class UserInsertCommandValidator : AbstractValidator<UserInsertCommand>
    {
        public UserInsertCommandValidator()
        {
            Include(new BaseUserCommandValidator());
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").Equal(x => x.ConfirmPassword).WithMessage("Password and confirmation must be the same");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required");
        }
    }
}
