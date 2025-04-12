using Domain.Entities;

namespace Repository.Repositories.Authors
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(CurotecDbContext context) : base(context)
        {

        }
    }
}
