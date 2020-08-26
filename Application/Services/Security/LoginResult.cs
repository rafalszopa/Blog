namespace Application.Services.Security
{
    public enum LoginResult
    {
        Ok = 0,
        AccountDoesNotExist = 1,
        IncorrectPassword = 2
    }

    //public class LoginResult
    //{
    //    public string FirstName { get; set; }

    //    public string LastName { get; set; }

    //    public string Email { get; set; }

    //    public string PasswordHash { get; set; }

    //    public bool Locked { get; set; }
    //}
}
