using MediatR;
using Voter.Dto.Responses.Poll;

namespace Voter.Dto.Requests.Poll
{
    public class GetPollRequest : IRequest<GetPollResponse>
    {
        public string Id { get; set; }
    }
}
