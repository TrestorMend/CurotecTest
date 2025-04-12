using Communique.Repository;
using Domain.Entities;

namespace Repository.Repositories.Books
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(CurotecDbContext context) : base(context)
        {

        }
    }
}
