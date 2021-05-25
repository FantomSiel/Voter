using MediatR;
using System.Collections.Generic;
using Voter.Dto.Dtos;

namespace Voter.Dto.Requests.Question
{
    public class AddQuestionRequest : IRequest<Unit>
    {
        public string PollId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public IEnumerable<VariantDto> Variants { get; set; }
    }
}
