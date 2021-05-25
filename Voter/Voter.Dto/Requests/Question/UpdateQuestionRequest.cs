using MediatR;

namespace Voter.Dto.Requests.Question
{
    public class UpdateQuestionRequest : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
