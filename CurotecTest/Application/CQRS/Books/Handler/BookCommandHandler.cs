using Application.CQRS.Books.Commands;
using Application.Responses;
using Application.Services.Books;
using Application.Services.Books.DTO.Request;
using Application.Services.Books.DTO.Response;
using AutoMapper;
using MediatR;
using Repository;
using Serilog;

namespace Application.CQRS.Books.Handler
{
    public class BookCommandHandler : CommandQueryHandlerBase,
        IRequestHandler<BookInsertCommand, ResponseState>,
        IRequestHandler<BookUpdateCommand, ResponseState>,
        IRequestHandler<BookDeleteCommand, ResponseState>
    {
        private readonly IBookService _bookService;

        public BookCommandHandler(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IResponseState responseState,
            IBookService bookService) : base(mapper, logger, mediator, unitOfWork, responseState)
        {
            _bookService = bookService;
        }

        public async Task<ResponseState> Handle(
        BookInsertCommand request,
        CancellationToken cancellationToken)
        {
            var bookDTO =
                _mapper.Map<BookRequestDTO>(request);

            var ret = await _bookService.Add(bookDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(_mapper.Map<BookResponseDTO>(ret));
        }

        public async Task<ResponseState> Handle(
        BookUpdateCommand request,
        CancellationToken cancellationToken)
        {
            var bookDTO =
                _mapper.Map<BookRequestDTO>(request);

            var ret = await _bookService.Update(bookDTO);

            if (ret == null || _responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(_mapper.Map<BookResponseDTO>(ret));
        }

        public async Task<ResponseState> Handle(
        BookDeleteCommand request,
        CancellationToken cancellationToken)
        {
            await _bookService.Delete(request.Id);

            if (_responseState.HasNotifications)
            {
                return _responseState.Response();
            }

            await _unitOfWork.CommitAsync();

            return _responseState.Response(request.Id);
        }
    }
}
