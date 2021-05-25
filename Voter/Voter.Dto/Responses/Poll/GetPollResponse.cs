using System;
using System.Collections.Generic;
using Voter.Dto.Dtos;

namespace Voter.Dto.Responses.Poll
{
    public class GetPollResponse
    {
        public string UserId { get; set; }
        public string PollId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
