using MediatR;
using System;
using System.Collections.Generic;
using Voter.Dto.Dtos;
using Voter.Dto.Responses.Poll;

namespace Voter.Dto.Requests.Poll
{
    public class AddPollRequest : IRequest<AddPollResponse>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
