using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dal.Models;
using Voter.Dto.Requests.Question;

namespace Voter.Core.Handlers
{
    public class AddQuestionRequestHandler : IRequestHandler<AddQuestionRequest>
    {
        private readonly VoteDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddQuestionRequestHandler(VoteDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddQuestionRequest request, CancellationToken cancellationToken)
        {
            var pollId = Guid.Parse(request.PollId);

            var poll = await _dbContext.Polls
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(x => x.PollId == pollId);
            if (poll == null)
            {
                throw Error.GetException(ErrorType.PollNotFound, request.PollId);
            }

            if (poll.Questions.Any(x => request.Text.Equals(x.Text, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw Error.GetException(ErrorType.QuestionAlreadyExists, request.Text);
            }

            request.Type = request.Type.ToLower();

            poll.Questions.Add(new Question
            {
                Text = request.Text,
                Type = (QuestionType)Enum.Parse(typeof(QuestionType), char.ToUpper(request.Type[0]) + request.Type.Substring(1)),
                Variants = _mapper.Map<ICollection<Variant>>(request.Variants)
            });

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
