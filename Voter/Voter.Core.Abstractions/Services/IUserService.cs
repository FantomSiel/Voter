using System;
using System.Threading.Tasks;
using Voter.Dto.Dtos;

namespace Voter.Core.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id);
    }
}
