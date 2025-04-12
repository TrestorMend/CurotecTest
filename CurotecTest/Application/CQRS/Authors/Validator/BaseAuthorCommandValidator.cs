using Application.CQRS.Authors.Commands;
using FluentValidation;

namespace Application.CQRS.Authors.Validator
{
    public class BaseAuthorCommandValidator : AbstractValidator<BaseAuthorCommand>
    {
        public BaseAuthorCommandValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("Max value for First Name is 50 characters")
                .NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).MaximumLength(150).WithMessage("Max value for Last Name is 150 characters")
                .NotEmpty().WithMessage("Last Name is required");
        }
    }
}