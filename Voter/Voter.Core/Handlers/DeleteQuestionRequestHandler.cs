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
    public class DeleteQuestionRequestHandler : IRequestHandler<DeleteQuestionRequest>
    {
        private readonly VoteDbContext _dbContext;

        public DeleteQuestionRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteQuestionRequest request, CancellationToken cancellationToken)
        {
            var questionId = Guid.Parse(request.Id);

            var item = await _dbContext.Questions.FirstOrDefaultAsync(x => x.QuestionId == questionId);
            if (item == null)
            {
                throw Error.GetException(ErrorType.QuestionNotFound, request.Id);
            }

            _dbContext.Questions.Remove(item);

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
