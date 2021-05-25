using MediatR;
using Voter.Dto.Responses.Question;

namespace Voter.Dto.Requests.Question
{
    public class GetQuestionRequest : IRequest<GetQuestionResponse>
    {
        public string Id { get; set; }
    }
}
