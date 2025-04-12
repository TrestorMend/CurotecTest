namespace Application.Services.Users.DTO.Request
{
    public class ListUserRequestDTO : ListDTOBase
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

    }
}
