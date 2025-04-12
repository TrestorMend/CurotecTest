namespace CurotecTest.ViewModels.Auth
{
    public class LoginRequestViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return false;

            return true;
        }
    }
}
