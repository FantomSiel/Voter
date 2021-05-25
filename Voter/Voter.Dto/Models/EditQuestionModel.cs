using System.Collections.Generic;
using Voter.Dto.Dtos;

namespace Voter.Dto.Models
{
    public class EditQuestionModel
    {
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public List<VariantDto> Variants { get; set; }
    }
}
