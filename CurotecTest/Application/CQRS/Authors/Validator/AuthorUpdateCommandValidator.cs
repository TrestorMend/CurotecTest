using Application.CQRS.Authors.Commands;
using FluentValidation;

namespace Application.CQRS.Authors.Validator
{
    public class AuthorUpdateCommandValidator : AbstractValidator<AuthorUpdateCommand>
    {
        public AuthorUpdateCommandValidator()
        {
            Include(new BaseAuthorCommandValidator());
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
