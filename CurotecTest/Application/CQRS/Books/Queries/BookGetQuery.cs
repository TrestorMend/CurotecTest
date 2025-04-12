namespace Application.CQRS.Books.Queries
{
    public class BookGetQuery : ListCommandQueryBase
    {
        public string? Title { get; set; }
        public int? YearPublished { get; set; }
        public int? AuthorId { get; set; }
    }
}
