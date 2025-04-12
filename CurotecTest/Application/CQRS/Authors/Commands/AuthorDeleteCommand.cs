using Application.Responses;
using MediatR;

namespace Application.CQRS.Authors.Commands
{
    public class AuthorDeleteCommand : IRequest<ResponseState>
    {
        public int Id { get; init; }
    }
}
