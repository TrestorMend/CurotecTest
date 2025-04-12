using Application.Responses;
using MediatR;

namespace Application.CQRS.Users.Commands
{
    public class UserDeleteCommand : IRequest<ResponseState>
    {
        public int Id { get; init; }
    }
}
