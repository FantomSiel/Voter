using MediatR;

namespace Voter.Dto.Requests.Poll
{
    public class DeletePollRequest : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
