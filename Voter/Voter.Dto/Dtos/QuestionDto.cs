using System.Collections.Generic;

namespace Voter.Dto.Dtos
{
    public class QuestionDto
    {
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public List<VariantDto> Variants { get; set; }
    }
}
