using Infrastructure.Authentication;
using Infrastructure.Security.Authentication;
using NUnit.Framework;

namespace Infrastucture.Security.Tests.Authentication
{
    [TestFixture]
    public class PasswordHasherTests
    {
        [TestCase(null, null)]
        [TestCase("", new byte[0])]
        public void VerifyPassword_Should_ReturnFail_When_PassedNullOrEmptyParams(string password, byte[] hash)
        {
            // Arrange ans Act
            var sut = new PasswordHasher();
            var result = sut.VerifyPassword(password, hash);

            // Assert
            Assert.That(result, Is.EqualTo(PasswordVerificationResult.Failed));
        }

        [Test]
        public void VerifyPassword_Should_ReturnSuccess_When_PasswordIsCorrect()
        {
            // Arrnge
            const string plainPassword = "Admin_123";
            var sut = new PasswordHasher();
            byte[] hashedPassword = sut.HashPassword(plainPassword);

            // Act
            var result = sut.VerifyPassword(plainPassword, hashedPassword);

            // Assert
            Assert.That(result, Is.EqualTo(PasswordVerificationResult.Success));
        }

        [Test]
        public void VerifyPassword_Should_ReturnFail_When_PasswordIsWrong()
        {
            // Arrnge
            const string correctPlainPassword = "Admin_123";
            const string wrongPlainPassword = "Admin123";
            var sut = new PasswordHasher();
            byte[] hashedPassword = sut.HashPassword(correctPlainPassword);

            // Act
            var result = sut.VerifyPassword(wrongPlainPassword, hashedPassword);

            // Assert
            Assert.That(result, Is.EqualTo(PasswordVerificationResult.Failed));
        }

        [TestCase(null)]
        [TestCase("")]
        public void HashPassword_Should_ReturnEmptyByteArray_When_PassedNullOrEmptyPassword(string password)
        {
            // Arrange ans Act
            var sut = new PasswordHasher();
            var result = sut.HashPassword(password);

            // Assert
            Assert.That(result, Is.EquivalentTo(new byte[0]));
        }

        [Test]
        public void HashPassword_Should_ReturnByteArray()
        {
            // Arrange ans Act
            var sut = new PasswordHasher();
            var result = sut.HashPassword("Admin_123");

            // Assert
            Assert.That(result, Is.Not.Empty);
        }
    }
}
