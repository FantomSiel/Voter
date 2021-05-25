using System;

namespace Voter.Dal.Models
{
    public class AnswerAggregate
    {
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid VariantId { get; set; }
    }
}
