using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voter.Dal.Models;

namespace Voter.Dal.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.Mail)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(x => x.Sex)
                .IsRequired()
                .HasMaxLength(8);

            builder
                .HasMany("Voter.Dal.Models.Poll", "Polls")
                .WithOne("User")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
