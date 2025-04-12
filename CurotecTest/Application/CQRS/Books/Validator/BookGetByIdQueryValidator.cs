using Application.CQRS.Books.Queries;
using FluentValidation;

namespace Application.CQRS.Books.Validator
{
    public class BookGetByIdQueryValidator : AbstractValidator<BookGetByIdQuery>
    {
        public BookGetByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
