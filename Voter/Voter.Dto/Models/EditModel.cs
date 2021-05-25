using System.Collections.Generic;
using Voter.Dto.Dtos;

namespace Voter.Dto.Models
{
    public class EditModel
    {
        public string PollId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
