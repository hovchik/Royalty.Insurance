using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CoverageTypeConfiguration : IEntityTypeConfiguration<CoverageType>
    {
        public void Configure(EntityTypeBuilder<CoverageType> builder)
        {
            builder.ToTable("CoverageTypes");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
