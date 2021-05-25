using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Voter.Core.Abstractions.Services;
using Voter.Dal;
using Voter.Dto.Dtos;

namespace Voter.Core.Services
{
    public class UserService : IUserService
    {
        private readonly VoteDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(VoteDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> GetById(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            var res = user == null
                ? null
                : _mapper.Map<UserDto>(user);
            return res;
        }
    }
}
