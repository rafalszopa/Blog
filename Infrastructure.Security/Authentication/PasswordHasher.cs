using Infrastructure.Security.Authentication;
using Infrastructure.Security.Interfaces;
using Infrastructure.Security.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Linq;
using System.Security.Cryptography;

namespace Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        #region Private fields

        private const int SaltLength = 128 / 8;

        private const int IterationCount = 10_000;

        private const int HashLength = 256 / 8;

        private const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;

        #endregion
        #region Public methods

        public byte[] HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return new byte[0];
            }

            byte[] salt = GetSalt(SaltLength);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, IterationCount, HashLength);

            var passwordHash = new PasswordHash(Pbkdf2Prf, IterationCount, SaltLength, salt, subkey);

            return passwordHash.Bytes;
        }

        public PasswordVerificationResult VerifyPassword(string givenPassword, byte[] passwordHash)
        {
            if (string.IsNullOrEmpty(givenPassword) || !passwordHash?.Any() == true)
            {
                return PasswordVerificationResult.Failed;
            }

            var actualPassword = new PasswordHash(passwordHash);

            var subkey = KeyDerivation.Pbkdf2(givenPassword, actualPassword.Salt, Pbkdf2Prf, IterationCount, HashLength);

            var actualPasswordHash = new PasswordHash(Pbkdf2Prf, IterationCount, SaltLength, actualPassword.Salt, subkey);

            return actualPassword == actualPasswordHash ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }

        #endregion
        #region Private methods

        private byte[] GetSalt(int length)
        {
            using(var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                byte[] salt;
                randomNumberGenerator.GetBytes(salt = new byte[length]);

                return salt;
            }
        }

        #endregion
    }
}
