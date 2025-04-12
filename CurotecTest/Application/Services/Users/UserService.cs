using Application.Responses;
using Application.Services.Users.DTO.Request;
using Application.Services.Users.DTO.Response;
using AutoMapper;
using Domain.Entities;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Users;
using Repository.Session;
using Serilog;
using System.Linq.Expressions;

namespace Application.Services.Users
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(
            IMapper mapper,
            ILogger logger,
            IMediator mediator,
            IResponseState responseState,
            IUserRepository userRepository,
            IUserSession userSession,
            UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment) : base(mapper, logger, mediator, responseState, userSession, userRepository)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<User?> Add(UserRequestDTO dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null || await _userManager.FindByNameAsync(dto.UserName) != null)
            {
                _responseState.AddNotification(typeof(UserService), "User already registered on the database");
                return null;
            }

            var userEntity = _mapper.Map<User>(dto);

            await _userManager.CreateAsync(userEntity, dto.Password);

            return userEntity;
        }

        public async Task<User?> Update(UserRequestDTO dto)
        {
            var userDb = (await Find(x => x.Id == dto.Id.Value)).FirstOrDefault();

            if (userDb == null)
            {
                _responseState.AddNotification(typeof(UserService), "User not found or Id invalid");
                return null;
            }

            _mapper.Map(dto, userDb);;

            Update(userDb);

            return userDb;
        }

        public async Task Delete(int entityId)
        {
            var userDb = await _userManager.FindByIdAsync(entityId.ToString());

            if (userDb == null)
            {
                _responseState.AddNotification(typeof(UserService), "User not found or Id invalid");
                return;
            }

            await _userManager.UpdateAsync(userDb);
        }

        public async Task<UserResponseDTO?> GetById(int entityId)
        {
            var userDb = (await Find(x => x.Id == entityId)).FirstOrDefault();

            if (userDb == null)
            {
                _responseState.AddNotification(typeof(UserService), "User not found or Id invalid");
                return null;
            }

            var userResponse = _mapper.Map<UserResponseDTO>(userDb);

            return userResponse;
        }

        public async Task<ListUserResponseDTO> Get(ListUserRequestDTO request)
        {
            var listUsersDb = (await Find(GetFilter(request))).ToList();

            if (!string.IsNullOrEmpty(request.OrderByProperty))
            {
                if (request.OrderByDesc)
                    listUsersDb = listUsersDb.OrderByDescending(x => x.GetType().GetProperty(request.OrderByProperty).GetValue(x)).ToList();
                else
                    listUsersDb = listUsersDb.OrderBy(x => x.GetType().GetProperty(request.OrderByProperty).GetValue(x)).ToList();
            }

            var totalCount = listUsersDb.Count > 0 ? listUsersDb.Count : 1;

            if (request.PageIndex.HasValue && request.PageSize.HasValue)
                listUsersDb = listUsersDb.Skip((request.PageIndex.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value).ToList();

            var listUsersResponse = new ListUserResponseDTO(totalCount, request.PageSize.GetValueOrDefault(totalCount), request.PageIndex.GetValueOrDefault(1));

            listUsersResponse.Items.AddRange(from User in listUsersDb
                                             select _mapper.Map<UserResponseDTO>(User));

            return listUsersResponse;
        }

        private Expression<Func<User, bool>> GetFilter(ListUserRequestDTO query)
        {
            var predicate = PredicateBuilder.New<User>(true);

            if (!string.IsNullOrEmpty(query.Name))
                predicate.And(x => x.Name.Contains(query.Name));

            if (!string.IsNullOrEmpty(query.UserName))
                predicate.And(x => x.UserName.Contains(query.UserName));

            if (!string.IsNullOrEmpty(query.Email))
                predicate.And(x => x.Email.Contains(query.Email));

            return predicate;
        }

    }
}
