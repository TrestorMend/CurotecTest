using Application.CQRS.Users.Commands;
using FluentValidation;

namespace Application.CQRS.Users.Validator
{
    public class BaseUserCommandValidator : AbstractValidator<BaseUserCommand>
    {
        public BaseUserCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Max value for Name is 200 characters")
                .NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).MaximumLength(200).WithMessage("Max value for Email is 200 characters")
                .NotEmpty().WithMessage("Email is required")
                .Matches(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$").WithMessage("Email inválido");
            RuleFor(x => x.UserName).MaximumLength(100).WithMessage("Max value for UserName is 100 characters")
                .NotEmpty().WithMessage("UserName is required");
        }
    }
}