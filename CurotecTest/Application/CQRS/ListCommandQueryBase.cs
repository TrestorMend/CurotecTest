using Application.Responses;
using MediatR;

namespace Application.CQRS
{
    public class ListCommandQueryBase : IRequest<ResponseState>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string? OrderByProperty { get; set; }
        public bool OrderByDesc { get; set; }
    }
}
