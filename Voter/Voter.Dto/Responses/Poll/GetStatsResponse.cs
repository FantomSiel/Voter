using System.Collections.Generic;

namespace Voter.Dto.Responses.Poll
{
    public class GetStatsResponse
    {
        public string Name { get; set; }
        public List<StatsQuestionItem> Questions { get; set; }
    }

    public class StatsQuestionItem
    {
        public string Text { get; set; }
        public List<StatsVariantItem> Variants { get; set; }
    }

    public class StatsVariantItem
    {
        public string Text { get; set; }
        public int Percentage { get; set; }
    }
}
