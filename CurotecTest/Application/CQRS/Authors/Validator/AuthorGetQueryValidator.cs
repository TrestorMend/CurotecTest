using Application.CQRS.Authors.Queries;
using FluentValidation;

namespace Application.CQRS.Authors.Validator
{
    public class AuthorGetQueryValidator : AbstractValidator<AuthorGetQuery>
    {
        public AuthorGetQueryValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("Max value for First Name is 50 characters");
            RuleFor(x => x.LastName).MaximumLength(150).WithMessage("Max value for Last Name is 150 characters");
        }
    }
}
