using MediatR;
using Voter.Dto.Responses.Poll;

namespace Voter.Dto.Requests.Poll
{
    public class GetStatsRequest : IRequest<GetStatsResponse>
    {
        public string Id { get; set; }
    }
}
