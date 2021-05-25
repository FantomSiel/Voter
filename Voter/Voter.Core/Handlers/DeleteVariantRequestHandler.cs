using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dto.Requests.Variant;

namespace Voter.Core.Handlers
{
    public class DeleteVariantRequestHandler : IRequestHandler<DeleteVariantRequest>
    {
        private readonly VoteDbContext _dbContext;

        public DeleteVariantRequestHandler(VoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteVariantRequest request, CancellationToken cancellationToken)
        {
            var variantId = Guid.Parse(request.Id);

            var item = await _dbContext.Variants.FirstOrDefaultAsync(x => x.VariantId == variantId);
            if (item == null)
            {
                throw Error.GetException(ErrorType.VariantNotFound, request.Id);
            }

            _dbContext.Variants.Remove(item);

            await _dbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
