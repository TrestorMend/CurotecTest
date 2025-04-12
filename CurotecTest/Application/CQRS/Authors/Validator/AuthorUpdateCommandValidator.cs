using Application.CQRS.Books.Commands;
using Application.CQRS.Books.Validator;
using FluentValidation;

namespace Communique.Application.CQRS.Books.Validator
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
