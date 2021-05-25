using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voter.Dal.Models;

namespace Voter.Dal.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.QuestionId);

            builder
                .HasMany("Voter.Dal.Models.Variant", "Variants")
                .WithOne("Question")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.QuestionId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(x => x.Type)
                .IsRequired();
        }
    }
}
