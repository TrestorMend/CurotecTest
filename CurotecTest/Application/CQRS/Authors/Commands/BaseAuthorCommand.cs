using Application.Responses;
using MediatR;

namespace Application.CQRS.Authors.Commands
{
    public class BaseAuthorCommand : IRequest<ResponseState>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
