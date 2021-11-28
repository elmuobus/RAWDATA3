using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services;
using WebApi.Services.UserServices;
using WebApi.Utils;

namespace WebApi.Middleware
{
    public static class JwtAuthMiddlewareExtension
    {
        public static IApplicationBuilder UseJwtAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthMiddleware>();
        }
    }
    
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserBusinessLayer _user;
        private readonly ConfigurationUtils _configuration;


        public JwtAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _user = new UserBusinessLayer();
            _configuration = new ConfigurationUtils(configuration);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var key = Encoding.UTF8.GetBytes(
                    _configuration.GetString("Auth:Secret", "No secret"));

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out var validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                var claim = jwtToken?.Claims.FirstOrDefault(x => x.Type == "id");
                if (claim != null)
                {
                    var username = claim.Value;
                    context.Items["User"] = _user.GetUser(username);
                }
            }
            catch
            {
                // ignored
            }

            await _next(context);
        }
    }
}