using Application.Responses;
using AutoMapper;
using MediatR;
using Repository;
using Serilog;

namespace Application.CQRS
{
    public abstract class CommandQueryHandlerBase
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly IMediator _mediator;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IResponseState _responseState;

        protected CommandQueryHandlerBase(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IResponseState responseState)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _responseState = responseState;
        }
    }
}
