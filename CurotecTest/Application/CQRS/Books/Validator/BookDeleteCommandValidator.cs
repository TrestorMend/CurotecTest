using Application.CQRS.Books.Commands;
using FluentValidation;

namespace Application.CQRS.Books.Validator
{
    public class BookDeleteCommandValidator : AbstractValidator<BookDeleteCommand>
    {
        public BookDeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
