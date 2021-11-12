using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.ToTable("Agencies");
            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(255).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(15);
            builder.Property(x => x.FaxNumber).HasMaxLength(15);
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

            builder.HasOne(x => x.CreatedByNavigation).WithMany(x => x.AgencyCreatedByNavigations);
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.AgencyUpdatedByNavigations);
        }
    }
}
