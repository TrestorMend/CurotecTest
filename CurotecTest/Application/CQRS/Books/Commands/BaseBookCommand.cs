using Application.Responses;
using MediatR;

namespace Application.CQRS.Books.Commands
{
    public class BaseBookCommand : IRequest<ResponseState>
    {
        public required string Title { get; set; }
        public int YearPublished { get; set; }
        public int AuthorId { get; set; }
    }
}
