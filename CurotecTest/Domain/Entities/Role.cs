using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        private ICollection<UserRole>? _userRoles;
        public virtual ICollection<UserRole> UserRoles
        {
            get => _userRoles ??= new List<UserRole>();
        }
    }
}
