using Application.CQRS.Books.Commands;
using FluentValidation;

namespace Application.CQRS.Books.Validator
{
    public class BookUpdateCommandValidator : AbstractValidator<BookUpdateCommand>
    {
        public BookUpdateCommandValidator()
        {
            Include(new BaseBookCommandValidator());
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}
