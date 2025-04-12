namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        private ICollection<Book>? _books;

        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public virtual ICollection<Book> Books
        {
            get => _books ??= [];
        }        
    }
}
