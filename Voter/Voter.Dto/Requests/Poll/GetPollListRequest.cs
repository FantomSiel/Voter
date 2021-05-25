using MediatR;
using Voter.Dto.Responses.Poll;

namespace Voter.Dto.Requests.Poll
{
    public class GetPollListRequest : IRequest<GetPollListResponse>
    {
        public int Limit { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public bool IncludeDescription { get; set; } = true;
        public string UserId { get; set; }
    }
}
