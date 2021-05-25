using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voter.Dal.Models;

namespace Voter.Dal.Configuration
{
    public class AnswerAggregateConfiguration : IEntityTypeConfiguration<AnswerAggregate>
    {
        public void Configure(EntityTypeBuilder<AnswerAggregate> builder)
        {
            builder.HasKey(x => new { x.UserId, x.QuestionId, x.VariantId });
        }
    }
}
