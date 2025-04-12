using Microsoft.AspNetCore.Http;
using Repository.Session;
using System.Security.Claims;

public class UserSession : IUserSession
{
    public UserSession(IHttpContextAccessor accessor)
    {
        UserId = Convert.ToInt32(accessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    public int UserId { get; }
}
