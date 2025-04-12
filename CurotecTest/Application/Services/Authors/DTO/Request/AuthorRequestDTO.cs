namespace Application.Services.Authors.DTO.Request
{
    public class AuthorRequestDTO
    {
        public int? Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}

