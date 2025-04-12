namespace Application.Services.Authors.DTO.Request
{
    public class ListAuthorRequestDTO : ListDTOBase
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}