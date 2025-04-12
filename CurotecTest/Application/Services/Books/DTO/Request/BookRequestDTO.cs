namespace Application.Services.Books.DTO.Request
{
    public class BookRequestDTO
    {
        public int? Id { get; set; }
        public required string Title { get; set; }
        public int YearPublished { get; set; }
        public int AuthorId { get; set; }
    }
}

