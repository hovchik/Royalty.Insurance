using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CommodityConfiguration : IEntityTypeConfiguration<Commodity>
    {
        public void Configure(EntityTypeBuilder<Commodity> builder)
        {
            builder.ToTable("Commodity");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

            builder.HasOne(x => x.CreateByNavigation).WithMany(x => x.CommodityCreateByNavigations).HasForeignKey(x => x.UpdatedBy);
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.CommodityUpdatedByNavigations).HasForeignKey(x => x.CreateBy);
        }
    }
}
