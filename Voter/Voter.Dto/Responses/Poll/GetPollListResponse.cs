using System.Collections.Generic;
using Voter.Dto.Dtos;

namespace Voter.Dto.Responses.Poll
{
    public class GetPollListResponse
    {
        public IEnumerable<PollDto> Polls { get; set; }
    }
}
