using System.Collections.Generic;
using Voter.Dto.Dtos;

namespace Voter.Dto.Models
{
    public class MainPageModel
    {
        public bool IsOwn { get; set; }
        public IEnumerable<PollDto> Polls { get; set; }
        public int Page { get; set; }
    }
}
