using Application.Services.Users.DTO.Request;
using Application.Services.Users.DTO.Response;
using Domain.Entities;

namespace Application.Services.Users
{
    public interface IUserService
    {
        public Task<User?> Add(UserRequestDTO dto);
        public Task<User?> Update(UserRequestDTO dto);
        public Task<UserResponseDTO?> GetById(int entityId);
        public Task<ListUserResponseDTO> Get(ListUserRequestDTO request);
        public Task Delete(int entityId);
    }
}
