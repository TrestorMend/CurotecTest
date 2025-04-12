namespace Domain.Entities
{
    public class Book : BaseEntity
    { 
        public required string Title { get; set; }
        public int YearPublished { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
