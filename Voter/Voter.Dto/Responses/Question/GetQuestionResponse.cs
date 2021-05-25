using System;
using System.Collections.Generic;
using Voter.Dto.Dtos;

namespace Voter.Dto.Responses.Question
{
    public class GetQuestionResponse
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public List<VariantDto> Variants { get; set; }
    }
}
