using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class InsuredCoverageConfiguration : IEntityTypeConfiguration<InsuredCoverage>
    {
        public void Configure(EntityTypeBuilder<InsuredCoverage> builder)
        {
            builder.ToTable("InsuredCoverages");
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.Limit).HasDefaultValueSql("(1)");

            builder.HasOne(x => x.Insured).WithMany(x => x.InsuredCoverages).HasForeignKey(x => x.InsuredId);
            builder.HasOne(x => x.Coverage).WithMany(x => x.InsuredCoverages).HasForeignKey(x => x.CoverageId);
        }
    }
}
