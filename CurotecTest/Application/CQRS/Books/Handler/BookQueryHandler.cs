using Application.CQRS;
using Application.CQRS.Books.Queries;
using Application.Responses;
using Application.Services.Books;
using Application.Services.Books.DTO.Request;
using AutoMapper;
using MediatR;
using Repository;
using Serilog;

namespace Communique.Application.CQRS.Books.Handler
{
    public class BookQueryHandler : CommandQueryHandlerBase,
        IRequestHandler<BookGetByIdQuery, ResponseState>,
        IRequestHandler<BookGetQuery, ResponseState>
    {
        private readonly IBookService _bookService;

        public BookQueryHandler(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IResponseState responseState,
            IBookService BookService) : base(mapper, logger, mediator, unitOfWork, responseState)
        {
            _bookService = BookService;
        }

        public async Task<ResponseState> Handle(
        BookGetByIdQuery request,
        CancellationToken cancellationToken)
        {
            var ret = await _bookService.GetById(request.Id);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            return _responseState.Response(ret);
        }

        public async Task<ResponseState> Handle(
        BookGetQuery request,
        CancellationToken cancellationToken)
        {
            var listBookRequestDTO =
                   _mapper.Map<ListBookRequestDTO>(request);

            var ret = await _bookService.Get(listBookRequestDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            return _responseState.Response(ret);
        }
    }
}
