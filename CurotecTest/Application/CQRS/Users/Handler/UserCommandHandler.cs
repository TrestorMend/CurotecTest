using Application.CQRS.Users.Commands;
using Application.Responses;
using Application.Services.Users;
using Application.Services.Users.DTO.Request;
using Application.Services.Users.DTO.Response;
using AutoMapper;
using MediatR;
using Repository;
using Serilog;

namespace Application.CQRS.Users.Handler
{
    public class UserCommandHandler : CommandQueryHandlerBase,
        IRequestHandler<UserInsertCommand, ResponseState>,
        IRequestHandler<UserUpdateCommand, ResponseState>,
        IRequestHandler<UserDeleteCommand, ResponseState>
    {
        private readonly IUserService _userService;

        public UserCommandHandler(
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
        UserInsertCommand request,
        CancellationToken cancellationToken)
        {
            var userDTO =
                _mapper.Map<UserRequestDTO>(request);

            var ret = await _userService.Add(userDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(_mapper.Map<UserResponseDTO>(ret));
        }

        public async Task<ResponseState> Handle(
        UserUpdateCommand request,
        CancellationToken cancellationToken)
        {
            var userDTO =
                _mapper.Map<UserRequestDTO>(request);

            var ret = await _userService.Update(userDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(_mapper.Map<UserResponseDTO>(ret));
        }


        public async Task<ResponseState> Handle(
        UserDeleteCommand request,
        CancellationToken cancellationToken)
        {
            await _userService.Delete(request.Id);

            if (_responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(request.Id);
        }
    }
}
