using Application.Services.Books.DTO.Response;

namespace Application.Services.Authors.DTO.Response
{
    public class AuthorResponseDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<BookResponseDTO>? Books { get; set; }
    }
}
