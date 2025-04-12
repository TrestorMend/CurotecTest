namespace Application.Services.Books.DTO.Request
{
    public class ListBookRequestDTO : ListDTOBase
    {
        public string? Title { get; set; }
        public int? YearPublished { get; set; }
        public int? AuthorId { get; set; }
    }
}