namespace Application.CQRS.Users.Commands
{
    public class UserUpdateCommand : BaseUserCommand
    {
        public int Id { get; set; }
    }
}
