using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Attributes;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using WebApi.Utils;
using WebApi.ViewModels;

namespace WebApi.Controllers.UserControllers
{
    [ApiController]
    [Route(BaseUserRoute)]
    public class UsersController: ControllerBase
    {
        private const string BaseUserRoute = "api/users";
        private readonly UserBusinessLayer _userService;
        private readonly ConfigurationUtils _configuration;

        public UsersController(IConfiguration configuration)
        {
            _userService = new UserBusinessLayer();
            _configuration = new ConfigurationUtils(configuration);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (_userService.GetUser(dto.Username) != null)
                return BadRequest();

            var pwdSize = _configuration.GetInt("Auth:PasswordSize", "No password size");

            var salt = PasswordUtils.GenerateSalt(pwdSize);
            var pwd = PasswordUtils.HashPassword(dto.Password, salt, pwdSize);
            
            Console.WriteLine($"{salt}, {pwd}");

            if (_userService.CreateUser(dto.Username, pwd, salt) == null)
                return BadRequest();
            return CreatedAtRoute(null, new {dto.Username});
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _userService.GetUser(dto.Username);

            if (user == null)
                return BadRequest();
            
            var pwdSize = _configuration.GetInt("Auth:PasswordSize", "No password size");
            var secret = _configuration.GetString("Auth:Secret", "No secret");

            var password = PasswordUtils.HashPassword(dto.Password, user.Salt, pwdSize);
            if (password != user.Password)
                return BadRequest();

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Username)}),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);
            return Ok(new {dto.Username, token});
        }

        [Authorization]
        [HttpDelete]
        public IActionResult DeleteUser()
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.DeleteUser(user.Username);

                if (!isSucceeded)
                    return NotFound();
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}