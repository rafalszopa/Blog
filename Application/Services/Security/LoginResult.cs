namespace Application.Services.Security
{
    public class LoginResult
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool Locked { get; set; }
    }
}
