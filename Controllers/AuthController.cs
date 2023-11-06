using JwtAuthorization.Authentication;
using JwtAuthorization.Configurations;
using JwtAuthorization.Models;
using JwtAuthorization.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly JwtConfig _jwtConfig;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestViewModel requestUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userManager.FindByEmailAsync(requestUser.Email);
            if (existingUser is not null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                       "User with the following Email has already registered"
                    } 
                });
            }

            var newUser = new IdentityUser()
            {
                Email = requestUser.Email,
                UserName = requestUser.UserName
            };

            IdentityResult isCreated = await _userManager.CreateAsync(newUser, requestUser.Password);

            if (isCreated.Succeeded == false)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Failed to add the user"
                    }
                });
            }

            var token = JwtTokenGenerator.GenerateJwtToken(
                newUser, 
                _configuration.GetSection("JwtConfig:Secret").Value ?? string.Empty);

            return Ok(new AuthResult() { Result = true, Token = token });
        }
       
    }
}
