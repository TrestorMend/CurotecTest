namespace Application.CQRS.Authors.Queries
{
    public class AuthorGetQuery : ListCommandQueryBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
