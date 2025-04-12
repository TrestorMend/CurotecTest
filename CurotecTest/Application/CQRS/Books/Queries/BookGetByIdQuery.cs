using Application.Responses;
using MediatR;

namespace Application.CQRS.Books.Queries
{
    public class BookGetByIdQuery : IRequest<ResponseState>
    {
        public int Id { get; init; }
    }
}
