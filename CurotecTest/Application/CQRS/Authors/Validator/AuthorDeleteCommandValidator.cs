using Application.CQRS.Authors.Commands;
using FluentValidation;

namespace Application.CQRS.Authors.Validator
{
    public class AuthorDeleteCommandValidator : AbstractValidator<AuthorDeleteCommand>
    {
        public AuthorDeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
