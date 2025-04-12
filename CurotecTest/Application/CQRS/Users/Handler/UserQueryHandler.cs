using Application.CQRS.Users.Queries;
using Application.Responses;
using Application.Services.Users;
using Application.Services.Users.DTO.Request;
using AutoMapper;
using MediatR;
using Repository;
using Serilog;

namespace Application.CQRS.Users.Handler
{
    public class UserQueryHandler : CommandQueryHandlerBase,
        IRequestHandler<UserGetByIdQuery, ResponseState>,
        IRequestHandler<UserGetQuery, ResponseState>
    {
        private readonly IUserService _userService;

        public UserQueryHandler(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IResponseState responseState,
            IUserService userService) : base(mapper, logger, mediator, unitOfWork, responseState)
        {
            _userService = userService;
        }

        public async Task<ResponseState> Handle(
        UserGetByIdQuery request,
        CancellationToken cancellationToken)
        {
            var ret = await _userService.GetById(request.Id);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            return _responseState.Response(ret);
        }

        public async Task<ResponseState> Handle(
        UserGetQuery request,
        CancellationToken cancellationToken)
        {
            var listUserRequestDTO =
                   _mapper.Map<ListUserRequestDTO>(request);

            var ret = await _userService.Get(listUserRequestDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            return _responseState.Response(ret);
        }
    }
}
