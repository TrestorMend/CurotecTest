using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CurotecDbContext : IdentityDbContext<User, Role, int,
                                       IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                       IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public CurotecDbContext(DbContextOptions<CurotecDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CurotecDbContext).Assembly);
        }
    }
}
