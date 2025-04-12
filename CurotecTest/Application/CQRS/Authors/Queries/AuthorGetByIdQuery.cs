using Application.Responses;
using MediatR;

namespace Application.CQRS.Authors.Queries
{
    public class AuthorGetByIdQuery : IRequest<ResponseState>
    {
        public int Id { get; init; }
    }
}
