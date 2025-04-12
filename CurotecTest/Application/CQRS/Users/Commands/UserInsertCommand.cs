namespace Application.CQRS.Users.Commands
{
    public class UserInsertCommand : BaseUserCommand
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
