using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dto.Requests.Question;

namespace Voter.Core.Handlers
{
    public class UpdateQuestionRequestHandler : IRequestHandler<UpdateQuestionRequest>
    {
        private readonly VoteDbContext _dbContext;

        public UpdateQuestionRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateQuestionRequest request, CancellationToken cancellationToken)
        {
            var questionId = Guid.Parse(request.Id);

            var item = await _dbContext.Questions.FirstOrDefaultAsync(x => x.QuestionId == questionId);
            if (item == null)
            {
                throw Error.GetException(ErrorType.QuestionNotFound, request.Id);
            }

            item.Text = request.Text;

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
