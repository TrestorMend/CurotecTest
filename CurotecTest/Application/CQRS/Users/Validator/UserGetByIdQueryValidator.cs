using Application.CQRS.Users.Queries;
using FluentValidation;

namespace Application.CQRS.Users.Validator
{
    public class UserGetByIdQueryValidator : AbstractValidator<UserGetByIdQuery>
    {
        public UserGetByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
