using Application.CQRS.Users.Queries;
using FluentValidation;

namespace Application.CQRS.Users.Validator
{
    public class UserGetQueryValidator : AbstractValidator<UserGetQuery>
    {
        public UserGetQueryValidator()
        {
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Max value for Name is 200 characters");
            RuleFor(x => x.Email).MaximumLength(200).WithMessage("Max value for Email is 200 characters");
            RuleFor(x => x.UserName).MaximumLength(100).WithMessage("Max value for UserName is 100 characters");
        }
    }
}