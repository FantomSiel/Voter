using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voter.Core.Abstractions.Services;

namespace Voter.Service.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _secret = "12345678aabbccdd";

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_secret);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                    context.Items["User"] = await userService.GetById(userId);
                }
                catch
                {
                    // do nothing if jwt validation fails
                    // user is not attached to context so request won't have access to secure routes
                }
            }

            await _next(context);
        }
    }
}
