using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POC.Services.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace POC.Services.Commands.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountServiceController : ControllerBase
    {
        /// <summary>
        /// Logging Service
        /// </summary>
        //private readonly Logger.ILogger _logger;

        /// <summary>
        /// User Service
        /// </summary>
        IUserService _userService;

        /// <summary>
        /// DI
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="Identityservice"></param>
        public AccountServiceController(//Logger.ILogger logger,
                                        IUserService userService)
        {
            //_logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// GenerateToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("GenerateToken")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(UserLogin user)
        {
            //_logger.Info($"GenerateToken service started for user:{user.UserName}");
            if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
            {
                var userDetails = await _userService.Get(user);
                //_logger.Info($"User : {user.UserName} validated from database");

                if (userDetails.IsAuthentication)
                {
                    //_logger.Info($"User : {user.UserName} authentication done");
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userDetails.Username),
                new Claim(ClaimTypes.Role, userDetails.Role)
                };
                    var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                    var token = new JwtSecurityToken
                    (issuer: config["Jwt:Issuer"],
                        audience: config["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                            SecurityAlgorithms.HmacSha256)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    //_logger.Info($"Token has been generated for user : {user.UserName}");
                    userDetails.Token = tokenString;
                    return Ok(userDetails);
                }
                else
                {
                    //_logger.Info($"User : {user.UserName} - {userDetails.ErrorMessage}");
                    return BadRequest(userDetails);
                }
            }
            else
            {
                //_logger.Info($"User : {user.UserName} Invalid user credentials");
                return BadRequest("Invalid user credentials");
            }
        }
    }
}
