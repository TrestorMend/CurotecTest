using Application.CQRS.Authors.Commands;
using FluentValidation;

namespace Application.CQRS.Authors.Validator
{
    public class AuthorInsertCommandValidator : AbstractValidator<AuthorInsertCommand>
    {
        public AuthorInsertCommandValidator()
        {
            Include(new BaseAuthorCommandValidator());
        }
    }
}
