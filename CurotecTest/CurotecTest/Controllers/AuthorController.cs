using Application.CQRS.Authors.Commands;
using Application.CQRS.Authors.Queries;
using AutoMapper;
using CurotecTest.Controllers.Base;
using CurotecTest.ViewModels.Author;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace CurotecTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : BaseController
    {
        public AuthorController(
        ILogger logger,
        IMediator mediator,
        IMapper mapper) : base(logger, mediator, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthorById([FromQuery] AuthorGetViewModel authorGetViewModel)
        {
            try
            {
                var authorQuery =
                    _mapper.Map<AuthorGetQuery>(authorGetViewModel);

                var ret =
                        await _mediator.Send(authorQuery);

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
        public async Task<IActionResult> PostAuthor([FromBody] AuthorInsertViewModel authorCreateViewModel)
        {
            try
            {
                var authorCommand =
                    _mapper.Map<AuthorInsertCommand>(authorCreateViewModel);

                var ret =
                        await _mediator.Send(authorCommand);

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
        public async Task<IActionResult> PutAuthor([FromBody] AuthorUpdateViewModel authorUpdateViewModel)
        {
            try
            {
                var authorCommand =
                    _mapper.Map<AuthorUpdateCommand>(authorUpdateViewModel);

                var ret =
                        await _mediator.Send(authorCommand);

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
        public async Task<IActionResult> GetAuthorById([FromQuery] int id)
        {
            try
            {
                var authorQuery = new AuthorGetByIdQuery()
                {
                    Id = id
                };

                var ret =
                        await _mediator.Send(authorQuery);

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
        public async Task<IActionResult> DeleteAuthorById([FromQuery] int id)
        {
            try
            {
                var authorCommand = new AuthorDeleteCommand()
                {
                    Id = id
                };

                var ret =
                        await _mediator.Send(authorCommand);

                return GenerateResponse(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResult(ex));
            }
        }
    }
}
