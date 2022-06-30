using System.Security.Cryptography;
using VolcanoFinder.API.Services;

namespace VolcanoFinder.Tests
{
    public class PasswordHashServiceTests
    {
        private PasswordHashService _passwordHashService;
        private string _password;
        private string _incorrectPassword;

        public PasswordHashServiceTests()
        {
            _passwordHashService = new PasswordHashService();
            _password = "TestPassword";
            _incorrectPassword = "IncorrectPassword";
        }

        [Fact]
        public void CreatePasswordHash_CreateHashAndSalt_HashAndSaltNotNull()
        {
            // Act
            _passwordHashService.CreatePasswordHash(_password, out byte[]? _passwordHash, out byte[]? _passwordSalt);

            //Assert
            Assert.NotNull(_passwordHash);
            Assert.NotNull(_passwordSalt);
        }

        [Fact]
        public void VerifyPasswordHash_VerifyCorrectPassword_ResultMustBeTrue()
        {
            // Arrange
            byte[] _passwordHash;
            byte[] _passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_password));
                _passwordSalt = hmac.Key;
            }

            // Act
            var result = _passwordHashService.VerifyPasswordHash(_password, _passwordHash, _passwordSalt);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPasswordHash_VerifyWrongPassword_ResultMustBeFalse()
        {
            // Arrange
            byte[] _passwordHash;
            byte[] _passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_password));
                _passwordSalt = hmac.Key;
            }

            // Act
            var result = _passwordHashService.VerifyPasswordHash(_incorrectPassword, _passwordHash, _passwordSalt);

            //Assert
            Assert.False(result);
        }
    }
}
