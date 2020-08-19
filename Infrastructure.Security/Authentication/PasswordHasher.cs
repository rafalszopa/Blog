using Infrastructure.Security.Authentication;
using Infrastructure.Security.Interfaces;
using Infrastructure.Security.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltLength = 128 / 8;

        private const int IterationCount = 10_000;

        private const int HashLength = 256 / 8;

        private const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;

        public PasswordHash HashPassword(string password)    
        {
            byte[] salt = GetSalt(SaltLength);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, IterationCount, HashLength);

            var passwordHash = new PasswordHash(Pbkdf2Prf, IterationCount, SaltLength, salt, subkey);

            return passwordHash;
        }

        public PasswordVerificationResult VerifyPassword(string password, PasswordHash passwordHash)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (passwordHash == null)
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }

            return PasswordVerificationResult.Success;
        }

        private byte[] GetSalt(int length)
        {
            using(var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                byte[] salt;
                randomNumberGenerator.GetBytes(salt = new byte[length]);

                return salt;
            }
        }
    }
}
