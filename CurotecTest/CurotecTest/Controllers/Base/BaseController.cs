using Application.Responses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace CurotecTest.Controllers.Base
{
    [Authorize]
    public abstract class BaseController : ControllerBase
    {

        protected readonly ILogger _logger;
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        protected BaseController(
           ILogger logger,
           IMediator mediator,
           IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        protected IActionResult GenerateResponse(ResponseState commandResponse)
        {
            var resp = _mapper.Map<RequestResult>(commandResponse);

            if (resp.Success)
            {
                if (resp.Data == null)
                    return Ok();
                else
                    return Ok(resp.Data);
            }
            else
            {
                return BadRequest(resp);
            }
        }
    }
}
