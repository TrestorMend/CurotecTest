using Application.CQRS.Books.Commands;
using FluentValidation;

namespace Application.CQRS.Books.Validator
{
    public class BaseBookCommandValidator : AbstractValidator<BaseBookCommand>
    {
        public BaseBookCommandValidator()
        {
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Max value for Title is 50 characters")
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.YearPublished).NotEmpty().WithMessage("Year Published is required");
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Author is required");
        }
    }
}