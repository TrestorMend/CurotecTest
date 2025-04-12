using Application.Services.Login.DTO.Request;
using Application.Services.Login.DTO.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO dto)
        {
            var userDb = await _userManager.FindByNameAsync(dto.UserName);

            if (userDb == null)
                throw new Exception("User not found in database");

            var result = await _signInManager.CheckPasswordSignInAsync(userDb, dto.Password, false);

            if (!result.Succeeded)
                throw new Exception("Username or Password is incorrect");

            var expirationTime = DateTime.Now.AddHours(_configuration.GetValue<int>("Authorization:ExpirationTokenHours"));

            return new LoginResponseDTO()
            {
                Token = await GenerateJWToken(userDb, expirationTime),
                ExpirationDate = expirationTime
            };
        }

        private async Task<string> GenerateJWToken(User user, DateTime expirationDate)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authorization:SecretKey"));

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: expirationDate,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
