using Application.CQRS.Books.Commands;
using Application.CQRS.Books.Queries;
using AutoMapper;
using CurotecTest.Controllers.Base;
using CurotecTest.ViewModels.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace CurotecTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        public BookController(
        ILogger logger,
        IMediator mediator,
        IMapper mapper) : base(logger, mediator, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBookById([FromQuery] BookGetViewModel bookGetViewModel)
        {
            try
            {
                var bookQuery =
                    _mapper.Map<BookGetQuery>(bookGetViewModel);

                var ret =
                        await _mediator.Send(bookQuery);

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
        public async Task<IActionResult> PostBook([FromBody] BookInsertViewModel bookCreateViewModel)
        {
            try
            {
                var bookCommand =
                    _mapper.Map<BookInsertCommand>(bookCreateViewModel);

                var ret =
                        await _mediator.Send(bookCommand);

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
        public async Task<IActionResult> PutBook([FromBody] BookUpdateViewModel bookUpdateViewModel)
        {
            try
            {
                var bookCommand =
                    _mapper.Map<BookUpdateCommand>(bookUpdateViewModel);

                var ret =
                        await _mediator.Send(bookCommand);

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
        public async Task<IActionResult> GetBookById([FromQuery] int id)
        {
            try
            {
                var bookQuery = new BookGetByIdQuery()
                {
                    Id = id
                };

                var ret =
                        await _mediator.Send(bookQuery);

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
        public async Task<IActionResult> DeleteBookById([FromQuery] int id)
        {
            try
            {
                var bookCommand = new BookDeleteCommand()
                {
                    Id = id
                };

                var ret =
                        await _mediator.Send(bookCommand);

                return GenerateResponse(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResult(ex));
            }
        }
    }
}
