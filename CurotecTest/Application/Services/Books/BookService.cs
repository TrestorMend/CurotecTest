using Application.Responses;
using Application.Services.Books.DTO.Request;
using Application.Services.Books.DTO.Response;
using AutoMapper;
using Domain.Entities;
using LinqKit;
using MediatR;
using Repository.Repositories.Books;
using Repository.Session;
using Serilog;
using System.Linq.Expressions;

namespace Application.Services.Books
{
    public class BookService : ServiceBase<Book>, IBookService
    {
        public BookService(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IResponseState responseState,
            IUserSession userSession,
            IBookRepository bookRepository) : base(mapper, logger, mediator, responseState, userSession, bookRepository)
        {

        }

        public async Task<Book> Add(BookRequestDTO dto)
        {
            var bookEntity = _mapper.Map<Book>(dto);
            bookEntity.CreatedDate = bookEntity.UpdatedDate = DateTime.Now;
            bookEntity.CreatedUserId = bookEntity.UpdatedUserId = _userSession.UserId;

            await Add(bookEntity);

            return bookEntity;
        }

        public async Task<Book?> Update(BookRequestDTO dto)
        {
            var bookDb = await Get(dto.Id.Value);

            if (bookDb == null)
            {
                _responseState.AddNotification(typeof(BookService), "Book not found or Id invalid");
                return null;
            }

            _mapper.Map(dto, bookDb);
            bookDb.UpdatedDate = DateTime.Now;
            bookDb.UpdatedUserId = _userSession.UserId;

            Update(bookDb);

            return bookDb;
        }

        public async Task Delete(int entityId)
        {
            var bookDb = await Get(entityId);

            if (bookDb == null)
                _responseState.AddNotification(typeof(BookService), "Book not found or Id invalid");
            else
                Delete(bookDb);
        }

        public async Task<BookResponseDTO> GetById(int entityId)
        {
            var bookDb = await Get(entityId);

            if (bookDb == null)
            {
                _responseState.AddNotification(typeof(BookService), "Book not found or Id invalid");
                return null;
            }

            return _mapper.Map<BookResponseDTO>(bookDb);
        }

        public async Task<ListBookResponseDTO> Get(ListBookRequestDTO request)
        {
            var listBooksDb = (await Find(GetFilter(request))).ToList();

            if (!string.IsNullOrEmpty(request.OrderByProperty))
            {
                if (request.OrderByDesc)
                    listBooksDb = listBooksDb.OrderByDescending(x => x.GetType().GetProperty(request.OrderByProperty).GetValue(x)).ToList();
                else
                    listBooksDb = listBooksDb.OrderBy(x => x.GetType().GetProperty(request.OrderByProperty).GetValue(x)).ToList();
            }

            var totalCount = listBooksDb.Count > 0 ? listBooksDb.Count : 1;

            if (request.PageIndex.HasValue && request.PageSize.HasValue)
                listBooksDb = listBooksDb.Skip((request.PageIndex.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value).ToList();

            var listBooksResponse = new ListBookResponseDTO(totalCount, request.PageSize.GetValueOrDefault(totalCount), request.PageIndex.GetValueOrDefault(1));

            listBooksResponse.Items.AddRange(from book in listBooksDb
                                             select _mapper.Map<BookResponseDTO>(book));

            return listBooksResponse;
        }

        private Expression<Func<Book, bool>> GetFilter(ListBookRequestDTO query)
        {
            var predicate = PredicateBuilder.New<Book>(true);

            if (!string.IsNullOrEmpty(query.Title))
                predicate.And(x => x.Title.Contains(query.Title));

            if (query.AuthorId.HasValue)
                predicate.And(x => x.AuthorId == query.AuthorId);

            if (query.YearPublished.HasValue)
                predicate.And(x => x.YearPublished == query.YearPublished);

            return predicate;
        }
    }
}