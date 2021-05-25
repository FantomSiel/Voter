using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dto.Requests.Poll;
using Voter.Dto.Responses.Poll;

namespace Voter.Core.Handlers
{
    public class GetPollRequestHandler : IRequestHandler<GetPollRequest, GetPollResponse>
    {
        private readonly VoteDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPollRequestHandler(VoteDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetPollResponse> Handle(GetPollRequest request, CancellationToken cancellationToken)
        {
            var pollId = Guid.Parse(request.Id);

            var item = await _dbContext.Polls
                .Include(x => x.Questions)
                .ThenInclude(x => x.Variants)
                .FirstOrDefaultAsync(x => x.PollId == pollId);
            if (item == null)
            {
                throw Error.GetException(ErrorType.PollNotFound, request.Id);
            }

            return _mapper.Map<GetPollResponse>(item);
        }
    }
}
