using System;
using System.Collections.Generic;

namespace Voter.Dal.Models
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public QuestionType Type { get; set; }
        public string Text { get; set; }

        public Poll Poll { get; set; }

        public virtual ICollection<Variant> Variants { get; set; }
    }
}
