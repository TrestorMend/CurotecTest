using Application.Services.Login.DTO.Request;
using Application.Services.Login.DTO.Response;

namespace Application.Services.Login
{
    public interface ILoginService
    {
        public Task<LoginResponseDTO> Login(LoginRequestDTO dto);
    }
}
