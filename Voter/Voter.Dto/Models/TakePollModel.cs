using System.Collections.Generic;

namespace Voter.Dto.Models
{
    public class TakePollModel
    {
        public string PollId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TakePollQuestionItem> Questions { get; set; }
    }

    public class TakePollQuestionItem
    {
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public List<TakePollVariantItem> Variants { get; set; }
        public string SingleVariantId { get; set; }
    }

    public class TakePollVariantItem
    {
        public string VariantId { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }
}
