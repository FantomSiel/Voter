using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voter.Dal.Models;

namespace Voter.Dal.Configuration
{
    public class VariantConfiguration : IEntityTypeConfiguration<Variant>
    {
        public void Configure(EntityTypeBuilder<Variant> builder)
        {
            builder.HasKey(x => x.VariantId);

            builder.Property(x => x.VariantId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
