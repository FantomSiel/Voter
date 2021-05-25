using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dto.Requests.Poll;

namespace Voter.Core.Handlers
{
    public class UpdatePollRequestHandler : IRequestHandler<UpdatePollRequest>
    {
        private readonly VoteDbContext _dbContext;

        public UpdatePollRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdatePollRequest request, CancellationToken cancellationToken)
        {
            var pollId = Guid.Parse(request.Id);

            var item = await _dbContext.Polls.FirstOrDefaultAsync(x => x.PollId == pollId);
            if (item == null)
            {
                throw Error.GetException(ErrorType.PollNotFound, request.Id);
            }

            item.Name = request.Name;
            item.Description = request.Description;

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
