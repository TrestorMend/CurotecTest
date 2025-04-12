using Application.CQRS.Users.Commands;
using Application.CQRS.Users.Queries;
using AutoMapper;
using CurotecTest.Controllers.Base;
using CurotecTest.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace CurotecTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(
        ILogger logger,
        IMediator mediator,
        IMapper mapper) : base(logger, mediator, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserById([FromQuery] UserGetViewModel userGetViewModel)
        {
            try
            {
                var userQuery =
                    _mapper.Map<UserGetQuery>(userGetViewModel);

                var ret =
                        await _mediator.Send(userQuery);

                return GenerateResponse(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResult(ex));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUser([FromBody] UserInsertViewModel userCreateViewModel)
        {
            try
            {
                var userCommand =
                    _mapper.Map<UserInsertCommand>(userCreateViewModel);

                var ret =
                        await _mediator.Send(userCommand);

                return GenerateResponse(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResult(ex));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUser([FromBody] UserUpdateViewModel userUpdateViewModel)
        {
            try
            {
                var userCommand =
                    _mapper.Map<UserUpdateCommand>(userUpdateViewModel);

                var ret =
                        await _mediator.Send(userCommand);

                return GenerateResponse(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResult(ex));
            }
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            try
            {
                var userQuery = new UserGetByIdQuery()
                {
                    Id = id
                };

                var ret =
                        await _mediator.Send(userQuery);

                return GenerateResponse(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResult(ex));
            }
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUserById([FromQuery] int id)
        {
            try
            {
                var userCommand = new UserDeleteCommand()
                {
                    Id = id
                };

                var ret =
                        await _mediator.Send(userCommand);

                return GenerateResponse(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResult(ex));
            }
        }
    }
}
