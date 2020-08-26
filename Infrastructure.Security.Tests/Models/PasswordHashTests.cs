using Infrastructure.Authentication;
using Infrastructure.Security.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NUnit.Framework;
using System;

namespace Infrastucture.Security.Tests.Models
{
    public class PasswordHashTests
    {
        [TestCase(KeyDerivationPrf.HMACSHA1, 100, 64)]
        [TestCase(KeyDerivationPrf.HMACSHA256, 1_000, 256)]
        [TestCase(KeyDerivationPrf.HMACSHA512, 1_000, 512)]
        public void Ctors_ShouldCreateEqualObjects(KeyDerivationPrf keyDerivationPrf, int iterationCount, int saltLength)
        {
            // Arrange
            byte[] salt = this.GetRandomBytes(saltLength);
            byte[] hash = this.GetRandomBytes(saltLength * 2);

            // Act
            var sut_params_ctor = new PasswordHash(keyDerivationPrf, iterationCount, saltLength, salt, hash);
            var sut_bytes_ctor = new PasswordHash(sut_params_ctor.Bytes);

            // Assert
            Assert.That(sut_params_ctor, Is.EqualTo(sut_bytes_ctor));
        }

        [TestCase(null)]
        [TestCase(new byte[0])]
        public void CtorWhichTakesBytesArray_Should_ThrowException_When_BytesArrayIsNullOrEmpty(byte[] bytesArray)
        {
            Assert.That(() => new PasswordHash(bytesArray), Throws.ArgumentNullException);
        }

        [Test]
        public void CtorWhichTakesByteArray_Should_ThrowException_When_SaltLengthDoesMatchLengthOfSaltArray()
        {
            //Assert.That(() => new PasswordHash(bytesArray), Throws.ArgumentNullException);
        }

        [Test]
        public void Foo()
        {
            var passwordHasher = new PasswordHasher();
            var passwordHash = passwordHasher.HashPassword("Admin_123");

            string base64 = Convert.ToBase64String(passwordHash);
        }

        //[Test]
        //public void EqualityOperator_ShouldReturnTrue_WhenHashesAreSame()
        //{
        //    bool result = true;

        //    Assert.IsTrue(result);
        //}

        //[Test]
        //public void EqualityOperator_ShouldReturnFalse_WhenHashesAreNotSame()
        //{
        //    bool result = false;

        //    Assert.IsFalse(result);
        //}

        //public void Ctor_Should_TrowArgumentNullException_When_PassedNullArguments()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Ctor_Should_ThrowException_When_SaltLengthDoesNotMatchSalt()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void Equals_Should_ReturnTrue_When_HashesAreSame()
        //{
        //    // Arrange
        //    KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256;
        //    int iterationCount = 10_000;
        //    int saltLength = 128;
        //    byte[] salt = GetRandomBytes(saltLength);
        //    byte[] subkey = GetRandomBytes(128);

        //    // Act
        //    var sut1 = new PasswordHash(prf, iterationCount, saltLength, salt, subkey);
        //    var sut2 = new PasswordHash(prf, iterationCount, saltLength, salt, subkey);

        //    // Assert
        //    Assert.IsTrue(sut1.Equals(sut2));
        //    Assert.IsTrue(sut1 == sut2);
        //    Assert.IsFalse(sut1 != sut2);
        //}

        //[Test]
        //public void Equals_Should_ReturnFalse_When_HashesAreDifferent()
        //{
        //    // Arrange
        //    byte[] salt = GetRandomBytes(128);
        //    byte[] subkey = GetRandomBytes(128);

        //    // Act
        //    var sut1 = new PasswordHash(KeyDerivationPrf.HMACSHA256, 10_000, 128, salt, subkey);
        //    var sut2 = new PasswordHash(KeyDerivationPrf.HMACSHA256, 1_000, 128, salt, subkey);

        //    // Assert
        //    Assert.IsFalse(sut1 == sut2);
        //    Assert.IsTrue(sut1 != sut2);
        //}

        private byte[] GetRandomBytes(int bufferSize)
        {
            var random = new Random();
            var buffer = new byte[bufferSize];
            random.NextBytes(buffer);

            return buffer;
        }
    }
}
