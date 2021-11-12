using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class AchTypeConfiguration : IEntityTypeConfiguration<AchType>
    {
        public void Configure(EntityTypeBuilder<AchType> builder)
        {
            builder.ToTable("AchTypes");
            builder.Property(t => t.Type)
                   .HasMaxLength(5)
                   .IsRequired();

            builder.Property(t => t.Type)
                   .HasMaxLength(100);
        }
    }
}
