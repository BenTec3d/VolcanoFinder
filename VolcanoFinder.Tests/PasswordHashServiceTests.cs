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
        private PasswordHashService _passwordHashService;
        private string _password;
        private string _incorrectPassword;
        byte[]? _passwordHash;
        byte[]? _passwordSalt;

        public PasswordHashServiceTests()
        {
            _passwordHashService = new PasswordHashService();
            _password = "TestPassword";
            _incorrectPassword = "IncorrectPassword";
            _passwordHash = null;
            _passwordSalt = null;
        }

        [Fact]
        public void CreatePasswordHash_CreateHashAndSalt_HashAndSaltNotNull()
        {
            // Act
            _passwordHashService.CreatePasswordHash(_password, out _passwordHash, out _passwordSalt);

            //Assert
            Assert.NotNull(_passwordHash);
            Assert.NotNull(_passwordSalt);
        }

        [Fact]
        public void VerifyPasswordHash_VerifyCorrectPassword_ResultMustBeTrue()
        {
            // Arrange
            _passwordHashService.CreatePasswordHash(_password, out _passwordHash, out _passwordSalt);

            // Act
            var resultTrue = _passwordHashService.VerifyPasswordHash(_password, _passwordHash, _passwordSalt);

            //Assert
            Assert.True(resultTrue);
        }

        [Fact]
        public void VerifyPasswordHash_VerifyWrongPassword_ResultMustBeFalse()
        {
            // Arrange
            _passwordHashService.CreatePasswordHash(_password, out _passwordHash, out _passwordSalt);

            // Act
            var result = _passwordHashService.VerifyPasswordHash(_incorrectPassword, _passwordHash, _passwordSalt);

            //Assert
            Assert.False(result);
        }
    }
}
