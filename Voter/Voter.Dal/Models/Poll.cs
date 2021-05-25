using System;
using System.Collections.Generic;

namespace Voter.Dal.Models
{
    public class Poll
    {
        public Guid UserId { get; set; }
        public Guid PollId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public User User { get; set; }
    }
}
