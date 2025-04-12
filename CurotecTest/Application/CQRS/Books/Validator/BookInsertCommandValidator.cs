using Application.CQRS.Books.Commands;
using FluentValidation;

namespace Application.CQRS.Books.Validator
{
    public class BookInsertCommandValidator : AbstractValidator<BookInsertCommand>
    {
        public BookInsertCommandValidator()
        {
            Include(new BaseBookCommandValidator());
        }
    }
}
