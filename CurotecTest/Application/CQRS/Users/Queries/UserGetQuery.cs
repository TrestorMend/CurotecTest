namespace Application.CQRS.Users.Queries
{
    public class UserGetQuery : ListCommandQueryBase
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
