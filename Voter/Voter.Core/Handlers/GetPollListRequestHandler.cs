using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voter.Dal;
using Voter.Dto.Dtos;
using Voter.Dto.Requests.Poll;
using Voter.Dto.Responses.Poll;

namespace Voter.Core.Handlers
{
    public class GetPollListRequestHandler : IRequestHandler<GetPollListRequest, GetPollListResponse>
    {
        private readonly VoteDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPollListRequestHandler(VoteDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetPollListResponse> Handle(GetPollListRequest request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Polls.Where(x => true);

            if (!string.IsNullOrEmpty(request.UserId))
            {
                query = query.Where(x => x.UserId == Guid.Parse(request.UserId));
            }

            if (request.Offset > 0)
            {
                query = query.Skip(request.Offset);
            }

            if (request.Limit >= 0)
            {
                query = query.Take(request.Limit == 0 ? 20 : request.Limit);
            }

            var items = await query.Include(x => x.User).ToListAsync();
            if (!request.IncludeDescription)
            {
                foreach (var i in items)
                {
                    i.Description = null;
                }
            }

            var res = new GetPollListResponse()
            {
                Polls = _mapper.Map<IEnumerable<PollDto>>(items)
            };

            return res;
        }
    }
}
