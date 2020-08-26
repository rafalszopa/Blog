using Infrastructure.Security.Authentication;

namespace Infrastructure.Security.Interfaces
{
    public interface IPasswordHasher
    {
        byte[] HashPassword(string password);

        PasswordVerificationResult VerifyPassword(string password, byte[] passwordHash);
    }
}
