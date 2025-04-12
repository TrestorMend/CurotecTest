using Application.Responses;
using Application.Services.Authors.DTO.Request;
using Application.Services.Authors.DTO.Response;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using LinqKit;
using MediatR;
using Repository.Repositories.Authors;
using Repository.Session;
using Serilog;
using System.Linq.Expressions;

namespace Application.Services.Authors
{
    public class AuthorService : ServiceBase<Author>, IAuthorService
    {
        public AuthorService(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IResponseState responseState,
            IUserSession userSession,
            IAuthorRepository authorRepository) : base(mapper, logger, mediator, responseState, userSession, authorRepository)
        {

        }

        public async Task<Author> Add(AuthorRequestDTO dto)
        {
            var authorEntity = _mapper.Map<Author>(dto);
            authorEntity.CreatedDate = authorEntity.UpdatedDate = DateTime.Now;
            authorEntity.CreatedUserId = authorEntity.UpdatedUserId = _userSession.UserId;

            await Add(authorEntity);

            return authorEntity;
        }

        public async Task<Author?> Update(AuthorRequestDTO dto)
        {
            var authorDb = await Get(dto.Id.Value);

            if (authorDb == null)
            {
                _responseState.AddNotification(typeof(AuthorService), "Author not found or Id invalid");
                return null;
            }

            _mapper.Map(dto, authorDb);
            authorDb.UpdatedDate = DateTime.Now;
            authorDb.UpdatedUserId = _userSession.UserId;

            Update(authorDb);

            return authorDb;
        }

        public async Task Delete(int entityId)
        {
            var authorDb = await Get(entityId);

            if (authorDb == null)
                _responseState.AddNotification(typeof(AuthorService), "Author not found or Id invalid");
            else
                Delete(authorDb);
        }

        public async Task<AuthorResponseDTO> GetById(int entityId)
        {
            var authorDb = (await Find(x => x.Id == entityId, x => x.Books)).FirstOrDefault();

            if (authorDb == null)
            {
                _responseState.AddNotification(typeof(AuthorService), "Author not found or Id invalid");
                return null;
            }

            return _mapper.Map<AuthorResponseDTO>(authorDb);
        }

        public async Task<ListAuthorResponseDTO> Get(ListAuthorRequestDTO request)
        {
            var listAuthorsDb = (await Find(GetFilter(request), x => x.Books)).ToList();

            if (!string.IsNullOrEmpty(request.OrderByProperty))
            {
                if (request.OrderByDesc)
                    listAuthorsDb = listAuthorsDb.OrderByDescending(x => x.GetType().GetProperty(request.OrderByProperty).GetValue(x)).ToList();
                else
                    listAuthorsDb = listAuthorsDb.OrderBy(x => x.GetType().GetProperty(request.OrderByProperty).GetValue(x)).ToList();
            }

            var totalCount = listAuthorsDb.Count > 0 ? listAuthorsDb.Count : 1;

            if (request.PageIndex.HasValue && request.PageSize.HasValue)
                listAuthorsDb = listAuthorsDb.Skip((request.PageIndex.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value).ToList();

            var listAuthorsResponse = new ListAuthorResponseDTO(totalCount, request.PageSize.GetValueOrDefault(totalCount), request.PageIndex.GetValueOrDefault(1));

            listAuthorsResponse.Items.AddRange(from author in listAuthorsDb
                                             select _mapper.Map<AuthorResponseDTO>(author));

            return listAuthorsResponse;
        }

        private Expression<Func<Author, bool>> GetFilter(ListAuthorRequestDTO query)
        {
            var predicate = PredicateBuilder.New<Author>(true);

            if (!string.IsNullOrEmpty(query.FirstName))
                predicate.And(x => x.FirstName.Contains(query.FirstName));

            if (!string.IsNullOrEmpty(query.LastName))
                predicate.And(x => x.LastName.Contains(query.LastName));

            return predicate;
        }
    }
}