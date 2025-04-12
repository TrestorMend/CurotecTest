namespace Application.Services.Books.DTO.Response
{
    public class BookResponseDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int YearPublished { get; set; }
        public int AuthorId { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
