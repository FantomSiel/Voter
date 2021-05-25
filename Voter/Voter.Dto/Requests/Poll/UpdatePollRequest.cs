using MediatR;

namespace Voter.Dto.Requests.Poll
{
    public class UpdatePollRequest : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
