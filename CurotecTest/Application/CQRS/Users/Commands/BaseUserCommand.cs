using MediatR;
using Application.Responses;

namespace Application.CQRS.Users.Commands
{
    public class BaseUserCommand : IRequest<ResponseState>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
