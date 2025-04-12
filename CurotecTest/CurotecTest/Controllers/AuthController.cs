using Application.Services.Login;
using Application.Services.Login.DTO.Request;
using AutoMapper;
using CurotecTest.Controllers.Base;
using CurotecTest.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Communique.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController :  ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;

        public AuthController(ILoginService loginService,
            IMapper mapper) 
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestViewModel model)
        {
            try
            {
                if (!model.Validate())
                    throw new Exception("Invalid username or password");

                return Ok(await _loginService.Login(_mapper.Map<LoginRequestDTO>(model)));
            }
            catch (Exception ex)
            {
                return Unauthorized(new RequestResult(ex));
            }
        }
    }
}
