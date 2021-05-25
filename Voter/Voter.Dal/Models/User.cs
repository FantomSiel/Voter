using System;
using System.Collections.Generic;

namespace Voter.Dal.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string Sex { get; set; }
        public string HashedPassword { get; set; }

        public virtual ICollection<Poll> Polls { get; set; }
    }
}
