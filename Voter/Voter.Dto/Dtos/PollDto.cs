using System;

namespace Voter.Dto.Dtos
{
    public class PollDto
    {
        public string UserId { get; set; }
        public string PollId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
