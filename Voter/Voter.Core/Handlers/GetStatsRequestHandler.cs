using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dto.Requests.Poll;
using Voter.Dto.Responses.Poll;

namespace Voter.Core.Handlers
{
    public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, GetStatsResponse>
    {

        private readonly VoteDbContext _dbContext;

        public GetStatsRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetStatsResponse> Handle(GetStatsRequest request, CancellationToken cancellationToken)
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

            var response = new GetStatsResponse()
            {
                Name = item.Name,
                Questions = new List<StatsQuestionItem>()
            };

            foreach (var q in item.Questions)
            {
                var variants = new List<StatsVariantItem>();

                var total = _dbContext.AnswerAggregates
                        .Where(x => x.QuestionId == q.QuestionId)
                        .Select(x => x.UserId.ToString() + x.VariantId.ToString()).Distinct().Count();

                foreach (var v in q.Variants)
                {
                    var variantTotal = _dbContext.AnswerAggregates.Count(x => x.VariantId == v.VariantId);
                    variants.Add(new StatsVariantItem()
                    {
                        Text = v.Text,
                        Percentage = total == 0
                            ? 0
                            : (int)((double)variantTotal / (double)total * 100.0)
                    });
                }

                response.Questions.Add(new StatsQuestionItem()
                {
                    Text = q.Text,
                    Variants = variants
                });
            }

            return response;
        }
    }
}
