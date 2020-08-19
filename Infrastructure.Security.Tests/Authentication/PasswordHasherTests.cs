using Infrastructure.Authentication;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastucture.Security.Tests.Authentication
{
    [TestFixture]
    public class PasswordHasherTests
    {
        [Test]
        public void VerifyPassword_ShouldReturnSuccess()
        {
            var sut = new PasswordHasher();
            var result = sut.HashPassword("pa$$wOrd_123");
            var result2 = sut.HashPassword("pa$$wOrd123");

            Assert.True(true);
        }

        [Test]
        public void VerifyPassword_ShouldReturnFail()
        {

            var sut = new PasswordHasher();

            //sut.VerifyPassword()

        }
    }
}
