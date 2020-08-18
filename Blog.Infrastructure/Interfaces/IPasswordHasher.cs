using Infrastructure.Security.Authentication;
using Infrastructure.Security.Models;

namespace Infrastructure.Security.Interfaces
{
    public interface IPasswordHasher
    {
        PasswordHash HashPassword(string password);

        PasswordVerificationResult VerifyPassword(string password, PasswordHash passwordHash);
    }
}
