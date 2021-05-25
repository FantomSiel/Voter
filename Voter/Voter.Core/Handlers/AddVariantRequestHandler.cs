using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dal.Models;
using Voter.Dto.Requests.Variant;

namespace Voter.Core.Handlers
{
    public class AddVariantRequestHandler : IRequestHandler<AddVariantRequest>
    {
        private readonly VoteDbContext _dbContext;

        public AddVariantRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddVariantRequest request, CancellationToken cancellationToken)
        {
            var questionId = Guid.Parse(request.QuestionId);

            var question = await _dbContext.Questions
                .Include(x => x.Variants)
                .FirstOrDefaultAsync(x => x.QuestionId == questionId);
            if (question == null)
            {
                throw Error.GetException(ErrorType.VariantAlreadyExists, request.QuestionId);
            }

            if (question.Variants.Any(x => request.Text.Equals(x.Text, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw Error.GetException(ErrorType.VariantAlreadyExists, request.Text);
            }

            question.Variants.Add(new Variant
            {
                Text = request.Text
            });

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
