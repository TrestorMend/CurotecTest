using CurotecTest.ViewModels.User.Base;

namespace CurotecTest.ViewModels.User
{
    public class UserInsertViewModel : BaseUserViewModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
