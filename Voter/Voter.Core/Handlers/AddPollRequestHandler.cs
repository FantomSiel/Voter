using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dal.Models;
using Voter.Dto.Requests.Poll;
using Voter.Dto.Responses.Poll;

namespace Voter.Core.Handlers
{
    public class AddPollRequestHandler : IRequestHandler<AddPollRequest, AddPollResponse>
    {
        private readonly VoteDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddPollRequestHandler(VoteDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AddPollResponse> Handle(AddPollRequest request, CancellationToken cancellationToken)
        {
            if (_dbContext.Polls.Any(x => request.Name.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw Error.GetException(ErrorType.PollAlreadyExists, request.Name);
            }

            var item = new Poll()
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                Questions = _mapper.Map<ICollection<Question>>(request.Questions),
            };

            _dbContext.Add(item);

            await _dbContext.SaveChangesAsync();

            return new AddPollResponse()
            {
                PollId = item.PollId
            };
        }
    }
}
