using Application.Responses;
using MediatR;

namespace Application.CQRS.Books.Commands
{
    public class BookDeleteCommand : IRequest<ResponseState>
    {
        public int Id { get; init; }
    }
}
