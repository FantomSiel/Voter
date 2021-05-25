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
    public class DeletePollRequestHandler : IRequestHandler<DeletePollRequest>
    {
        private readonly VoteDbContext _dbContext;

        public DeletePollRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeletePollRequest request, CancellationToken cancellationToken)
        {
            var pollId = Guid.Parse(request.Id);

            var item = await _dbContext.Polls.FirstOrDefaultAsync(x => x.PollId == pollId);
            if (item == null)
            {
                throw Error.GetException(ErrorType.PollNotFound, request.Id);
            }

            _dbContext.Polls.Remove(item);

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
