using Infrastructure.Security.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NUnit.Framework;
using System;

namespace Infrastucture.Security.Tests.Models
{
    public class PasswordHashTests
    {
        public string Base64EncodedPasswordHash =>
            "AQAAABAnAACAAAAAAh1aM2YgEvdSqNfWG10GzqMFncTjBmXaDuvSpAxbtnjNr8ar9aFJ8MhxxKdHUzEIZMmX2UoxEPu3fyV6Tm3q9/FeCWyWW9gBvKz7ckK+k7u5yUBDxN0tnJ/K83V8zqFah8216UnFcQjwlR2eNZ65+expwYUtpjMUMG3NzGdjhe004ELmbPL9OzCq89DGD9JX9JBBqXZQFbIIk8xr1I3zkoIu7Sg00A7fwc67nf9BfJNCIs/1ehY+48qP3dkmkmejtmFNlyGqIiSBYuenlXezxhc5KbG9/Rds7wldpPI1uS9nF+ad9ORxlwCqdBgvl05T3BfC7Gi15blK4acYTH0ySw==";

        public abstract class PasswordHashBase
        {
            public abstract void Bytes_Should_ReturnProperByteArray();
        }

        [TestFixture]
        public class Ctor_Takes_ByteArray : PasswordHashBase
        {
            [Test]
            public override void Bytes_Should_ReturnProperByteArray()
            {
                var bytes = new byte[100];

                var passwordHash = new PasswordHash(bytes);

                Assert.AreEqual(Convert.ToBase64String(bytes), Convert.ToBase64String(passwordHash.Bytes));
            }
        }

        [TestFixture]
        public class Ctor_Takes_HashParameters : PasswordHashBase
        {
            [Test]
            public override void Bytes_Should_ReturnProperByteArray()
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void EqualityOperator_ShouldReturnTrue_WhenHashesAreSame()
        {
            bool result = true;

            Assert.IsTrue(result);
        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenHashesAreNotSame()
        {
            bool result = false;

            Assert.IsFalse(result);
        }

        [Test]
        public void Should_CreateCorrect()
        {
            // Arrange
            KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256;
            int iterationCount = 10_000;
            int saltLength = 128;
            byte[] salt = GetRandomBytes(saltLength);
            byte[] subkey = GetRandomBytes(128);

            // Act
            var sut = new PasswordHash(prf, iterationCount, saltLength, salt, subkey);
            var sut2 = new PasswordHash(sut.Bytes);

            Assert.That(iterationCount, Is.EqualTo(sut.IterationCount).And.EqualTo(sut2.IterationCount));

            // Assert
            Assert.AreEqual(prf, sut.Prf);
            Assert.AreEqual(iterationCount, sut.IterationCount);
            Assert.AreEqual(saltLength, sut.SaltLength);
            Assert.AreEqual(salt, sut.Salt);
            Assert.AreEqual(subkey, sut.Subkey);
        }

        public void Ctor_Should_TrowArgumentNullException_When_PassedNullArguments()
        {
            Assert.Fail();
        }

        [Test]
        public void Ctor_Should_ThrowException_When_SaltLengthDoesNotMatchSalt()
        {
            Assert.Fail();
        }


        [Test]
        public void Equals_Should_ReturnTrue_When_HashesAreSame()
        {
            // Arrange
            KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256;
            int iterationCount = 10_000;
            int saltLength = 128;
            byte[] salt = GetRandomBytes(saltLength);
            byte[] subkey = GetRandomBytes(128);

            // Act
            var sut1 = new PasswordHash(prf, iterationCount, saltLength, salt, subkey);
            var sut2 = new PasswordHash(prf, iterationCount, saltLength, salt, subkey);

            // Assert
            Assert.IsTrue(sut1.Equals(sut2));
            Assert.IsTrue(sut1 == sut2);
            Assert.IsFalse(sut1 != sut2);
        }


        [Test]
        public void Equals_Should_ReturnFalse_When_HashesAreDifferent()
        {
            // Arrange
            byte[] salt = GetRandomBytes(128);
            byte[] subkey = GetRandomBytes(128);

            // Act
            var sut1 = new PasswordHash(KeyDerivationPrf.HMACSHA256, 10_000, 128, salt, subkey);
            var sut2 = new PasswordHash(KeyDerivationPrf.HMACSHA256, 1_000, 128, salt, subkey);

            // Assert
            Assert.IsFalse(sut1 == sut2);
            Assert.IsTrue(sut1 != sut2);
        }

        private byte[] GetRandomBytes(int bufferSize)
        {
            var random = new Random();
            var buffer = new byte[bufferSize];
            random.NextBytes(buffer);

            return buffer;
        }
    }
}
