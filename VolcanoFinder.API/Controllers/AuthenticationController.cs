using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Models.Entities;
using VolcanoFinder.API.Services;

namespace VolcanoFinder.API.Controllers
{
    [Route("api/v:{version:apiVersion}/authentication")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IVolcanoFinderRepository _volcanoFinderRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IVolcanoFinderRepository volcanoFinderRepository, IPasswordHashService passwordHashService, IConfiguration configuration)
        {
            _volcanoFinderRepository = volcanoFinderRepository ?? throw new ArgumentNullException(nameof(volcanoFinderRepository));
            _passwordHashService = passwordHashService ?? throw new ArgumentNullException(nameof(passwordHashService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Authenticates a user and returns a JSON Web Token
        /// </summary>
        /// <param name="userDto">The userDTO for the authentication</param>
        /// <returns>Action Result of string containing a JSON Web Token</returns>
        /// <response code="200">Returns a JSON Web Token</response>
        /// <response code="400">A userDTO is required</response>
        /// <response code="401">No user found for provided userDTO</response>
        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(UserDto userDto)
        {
            var users = await _volcanoFinderRepository.GetUsersAsync(userDto.Name);

            if (users.IsNullOrEmpty())
                return Unauthorized();

            User? userEntity = null;
            foreach(User user in users)
            {
                if(_passwordHashService.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
                {
                    userEntity = user;
                    break;
                }
            }

            if(userEntity is null)
                return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>()
            {
                new Claim("sub", userEntity.Id.ToString()),
                new Claim("name", userEntity.Id.ToString()),
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }
    }
}
