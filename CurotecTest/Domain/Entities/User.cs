using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {
        private ICollection<UserRole>? _userRoles;
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles
        {
            get => _userRoles ??= new List<UserRole>();
        }
    }
}
