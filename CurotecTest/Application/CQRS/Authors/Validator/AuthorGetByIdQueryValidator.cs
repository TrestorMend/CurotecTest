using Application.CQRS.Authors.Queries;
using FluentValidation;

namespace Application.CQRS.Authors.Validator
{
    public class AuthorGetByIdQueryValidator : AbstractValidator<AuthorGetByIdQuery>
    {
        public AuthorGetByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
