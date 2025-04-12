namespace Application.CQRS.Authors.Commands
{
    public class AuthorUpdateCommand : BaseAuthorCommand
    {
        public int Id { get; set; }
    }
}
