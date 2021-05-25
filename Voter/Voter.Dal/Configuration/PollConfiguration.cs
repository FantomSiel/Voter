using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voter.Dal.Models;

namespace Voter.Dal.Configuration
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(x => x.PollId);

            builder
                .HasMany("Voter.Dal.Models.Question", "Questions")
                .WithOne("Poll")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.PollId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(512);
        }
    }
}
