using Application.CQRS.Authors.Queries;
using Application.Responses;
using Application.Services.Authors;
using Application.Services.Authors.DTO.Request;
using AutoMapper;
using MediatR;
using Repository;
using Serilog;

namespace Application.CQRS.Authors.Handler
{
    public class AuthorQueryHandler : CommandQueryHandlerBase,
        IRequestHandler<AuthorGetByIdQuery, ResponseState>,
        IRequestHandler<AuthorGetQuery, ResponseState>
    {
        private readonly IAuthorService _authorService;

        public AuthorQueryHandler(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IResponseState responseState,
            IAuthorService AuthorService) : base(mapper, logger, mediator, unitOfWork, responseState)
        {
            _authorService = AuthorService;
        }

        public async Task<ResponseState> Handle(
        AuthorGetByIdQuery request,
        CancellationToken cancellationToken)
        {
            var ret = await _authorService.GetById(request.Id);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            return _responseState.Response(ret);
        }

        public async Task<ResponseState> Handle(
        AuthorGetQuery request,
        CancellationToken cancellationToken)
        {
            var listAuthorRequestDTO =
                   _mapper.Map<ListAuthorRequestDTO>(request);

            var ret = await _authorService.Get(listAuthorRequestDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            return _responseState.Response(ret);
        }
    }
}
