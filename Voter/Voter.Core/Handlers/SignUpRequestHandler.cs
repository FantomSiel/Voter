using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dal.Models;
using Voter.Dto.Requests.User;

namespace Voter.Core.Handlers
{
    public class SignUpRequestHandler : IRequestHandler<SignUpRequest>
    {
        private readonly VoteDbContext _dbContext;

        public SignUpRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Mail.Equals(request.Login, StringComparison.InvariantCultureIgnoreCase));
            if (user != null)
            {
                throw Error.GetException(ErrorType.UserAlreadyExists, request.Login);
            }

            using (var sha256Hash = SHA256.Create())
            {
                string hash = GetHash(sha256Hash, request.Password);
                _dbContext.Users.Add(new User()
                {
                    Name = request.Name,
                    Age = request.Age,
                    Sex = request.Sex,
                    Country = request.Country,
                    Mail = request.Login,
                    HashedPassword = hash
                });
            }

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
