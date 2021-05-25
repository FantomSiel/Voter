using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;
using Voter.Dal;
using Voter.Dto.Requests.Question;
using Voter.Dto.Responses.Question;

namespace Voter.Core.Handlers
{
    public class GetQuestionRequestHandler : IRequestHandler<GetQuestionRequest, GetQuestionResponse>
    {
        private readonly VoteDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetQuestionRequestHandler(VoteDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetQuestionResponse> Handle(GetQuestionRequest request, CancellationToken cancellationToken)
        {
            var questionId = Guid.Parse(request.Id);

            var item = await _dbContext.Questions
                .Include(x => x.Variants)
                .FirstOrDefaultAsync(x => x.QuestionId == questionId);
            if (item == null)
            {
                throw Error.GetException(ErrorType.QuestionNotFound, request.Id);
            }

            return _mapper.Map<GetQuestionResponse>(item);
        }
    }
}
