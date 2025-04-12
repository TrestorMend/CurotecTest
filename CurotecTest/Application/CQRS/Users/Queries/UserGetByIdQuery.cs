using Application.Responses;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public class UserGetByIdQuery : IRequest<ResponseState>
    {
        public int Id { get; init; }
    }
}
