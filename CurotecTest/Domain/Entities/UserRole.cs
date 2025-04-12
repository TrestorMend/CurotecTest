using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual required User User { get; set; }
        public virtual required Role Role { get; set; }
    }
}
