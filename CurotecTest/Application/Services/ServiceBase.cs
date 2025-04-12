using Application.Responses;
using AutoMapper;
using MediatR;
using Repository;
using Repository.Session;
using Serilog;
using System.Linq.Expressions;

namespace Application.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly IMediator _mediator;
        protected readonly IResponseState _responseState;
        protected readonly IUserSession _userSession;
        protected readonly IRepositoryBase<T> _entityRepository;
        public ServiceBase(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IResponseState responseState,
            IUserSession userSession,
            IRepositoryBase<T> entityRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _responseState = responseState;
            _userSession = userSession;
            _entityRepository = entityRepository;
        }

        public async Task<T> Add(T entity)
        {
            return await _entityRepository.Add(entity);

        }

        public void Delete(T entity)
        {
            _entityRepository.Delete(entity);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _entityRepository.Find(predicate);
        }

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return _entityRepository.Find(predicate, includes);
        }

        public async Task<T?> Get(int id)
        {
            return await _entityRepository.Get(id);
        }

        public async Task<T?> Get(string key)
        {
            return await _entityRepository.Get(key);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entityRepository.GetAll();
        }

        public void Update(T entity)
        {
            _entityRepository.Update(entity);
        }

        public void Update(T entity, params Expression<Func<T, object>>[] properties)
        {
            _entityRepository.Update(entity, properties);
        }
    }
}
