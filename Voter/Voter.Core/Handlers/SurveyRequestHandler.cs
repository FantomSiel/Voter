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
using Voter.Dto.Requests.Poll;

namespace Voter.Core.Handlers
{
    public class SurveyRequestHandler : IRequestHandler<SurveyRequest, Unit>
    {
        private readonly VoteDbContext _dbContext;

        public SurveyRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(SurveyRequest request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.UserId);
            var questionIds = request.Responses.Select(x => Guid.Parse(x.QuestionId));

            if (await _dbContext.AnswerAggregates.AnyAsync(x
                => x.UserId == userId
                && questionIds.Any(id => x.QuestionId == id)))
            {
                throw Error.GetException(ErrorType.UserAlreadyCompletePoll, request.UserId);
            }

            var items = new List<AnswerAggregate>();
            foreach (var i in request.Responses)
            {
                foreach (var v in i.Variants)
                {
                    items.Add(new AnswerAggregate()
                    {
                        UserId = userId,
                        QuestionId = Guid.Parse(i.QuestionId),
                        VariantId = Guid.Parse(v)
                    });
                }
            }
            _dbContext.AnswerAggregates.AddRange(items);

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
