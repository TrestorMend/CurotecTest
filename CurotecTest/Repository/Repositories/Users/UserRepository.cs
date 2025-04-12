using Domain.Entities;

namespace Repository.Repositories.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(CurotecDbContext context) : base(context)
        {

        }
    }
}
