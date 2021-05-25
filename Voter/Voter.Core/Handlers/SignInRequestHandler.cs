using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dto.Requests.User;
using Voter.Dto.Responses.User;

namespace Voter.Core.Handlers
{
    public class SignInRequestHandler : IRequestHandler<SignInRequest, SignInResponse>
    {
        private readonly VoteDbContext _dbContext;

        public SignInRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SignInResponse> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var credentials = ExtractCredentials(request.Header).Split(':');
            var login = credentials[0];
            var password = credentials[1];

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Mail.Equals(login, StringComparison.InvariantCultureIgnoreCase));
            if (user == null)
            {
                throw Error.GetException(ErrorType.Unauthorized, login);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("12345678aabbccdd");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var strToken = tokenHandler.WriteToken(token);

            return new SignInResponse()
            {
                Name = user.Name,
                Mail = user.Mail,
                UserId = user.UserId.ToString(),
                Token = strToken
            };
        }

        private static string ExtractCredentials(string authHeader)
        {
            var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
            return Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(encodedUsernamePassword));
        }
    }
}
