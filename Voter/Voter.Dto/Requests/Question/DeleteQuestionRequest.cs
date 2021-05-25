using MediatR;

namespace Voter.Dto.Requests.Question
{
    public class DeleteQuestionRequest : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
