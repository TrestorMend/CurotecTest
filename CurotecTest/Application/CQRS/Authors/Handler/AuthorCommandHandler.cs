using Application.CQRS.Authors.Commands;
using Application.Responses;
using Application.Services.Authors;
using Application.Services.Authors.DTO.Request;
using Application.Services.Authors.DTO.Response;
using AutoMapper;
using MediatR;
using Repository;
using Serilog;

namespace Application.CQRS.Authors.Handler
{
    public class AuthorCommandHandler : CommandQueryHandlerBase,
        IRequestHandler<AuthorInsertCommand, ResponseState>,
        IRequestHandler<AuthorUpdateCommand, ResponseState>,
        IRequestHandler<AuthorDeleteCommand, ResponseState>
    {
        private readonly IAuthorService _authorService;

        public AuthorCommandHandler(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IResponseState responseState,
            IAuthorService authorService) : base(mapper, logger, mediator, unitOfWork, responseState)
        {
            _authorService = authorService;
        }

        public async Task<ResponseState> Handle(
        AuthorInsertCommand request,
        CancellationToken cancellationToken)
        {
            var authorDTO =
                _mapper.Map<AuthorRequestDTO>(request);

            var ret = await _authorService.Add(authorDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(_mapper.Map<AuthorResponseDTO>(ret));
        }

        public async Task<ResponseState> Handle(
        AuthorUpdateCommand request,
        CancellationToken cancellationToken)
        {
            var authorDTO =
                _mapper.Map<AuthorRequestDTO>(request);

            var ret = await _authorService.Update(authorDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(_mapper.Map<AuthorResponseDTO>(ret));
        }

        public async Task<ResponseState> Handle(
        AuthorDeleteCommand request,
        CancellationToken cancellationToken)
        {
            await _authorService.Delete(request.Id);

            if (_responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(request.Id);
        }
    }
}
