using Application.CQRS.Books.Queries;
using FluentValidation;

namespace Application.CQRS.Books.Validator
{
    public class BookGetQueryValidator : AbstractValidator<BookGetQuery>
    {
        public BookGetQueryValidator()
        {
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Max value for Title is 50 characters");
        }
    }
}
