using System;

namespace Voter.Dal.Models
{
    public class Variant
    {
        public Guid VariantId { get; set; }
        public string Text { get; set; }

        public Question Question { get; set; }
    }
}
