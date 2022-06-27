using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VolcanoFinder.API.Services;

namespace VolcanoFinder.Tests
{
    public class PasswordHashServiceTests
    {
        [Fact]
        public void CreatePasswordHash_CreateHashAndSalt_HashAndSaltNotNull()
        {
            // Arrange
            var passwordHashService = new PasswordHashService();
            var password = "TestPassword";
            byte[]? passwordHash = null;
            byte[]? passwordSalt = null;

            // Act
            passwordHashService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            //Assert
            Assert.NotNull(passwordHash);
            Assert.NotNull(passwordSalt);
        }

        [Fact]
        public void VerifyPasswordHash_VerifyCorrectPassword_ResultMustBeTrue()
        {
            // Arrange
            var passwordHashService = new PasswordHashService();

            var password = "TestPassword";
            byte[] passwordHash;
            byte[] passwordSalt;

            passwordHashService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act
            var resultTrue = passwordHashService.VerifyPasswordHash(password, passwordHash, passwordSalt);

            //Assert
            Assert.True(resultTrue);
        }

        [Fact]
        public void VerifyPasswordHash_VerifyWrongPassword_ResultMustBeFalse()
        {
            // Arrange
            var passwordHashService = new PasswordHashService();

            var password = "TestPassword";
            var wrongPassword = "WrongPassword";
            byte[] passwordHash;
            byte[] passwordSalt;

            passwordHashService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act
            var result = passwordHashService.VerifyPasswordHash(wrongPassword, passwordHash, passwordSalt);

            //Assert
            Assert.False(result);
        }
    }
}
