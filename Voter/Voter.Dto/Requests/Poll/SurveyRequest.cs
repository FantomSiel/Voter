using MediatR;
using System.Collections.Generic;

namespace Voter.Dto.Requests.Poll
{
    public class SurveyRequest : IRequest<Unit>
    {
        public string UserId { get; set; }
        public List<SurveyItem> Responses { get; set; }
    }

    public class SurveyItem
    {
        public string QuestionId { get; set; }
        public List<string> Variants { get; set; }
    }
}
