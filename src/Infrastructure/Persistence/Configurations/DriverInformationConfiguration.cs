using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DriverInformationConfiguration : IEntityTypeConfiguration<DriverInformation>
    {
        public void Configure(EntityTypeBuilder<DriverInformation> builder)
        {
            builder.ToTable("DriverInformation");
            builder.Property(x => x.DriverName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LicenseNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Accidents).HasColumnType("ntext");
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

            builder.HasOne(x => x.State).WithMany(x => x.DriverInformations);
            builder.HasOne(x => x.Insured).WithMany(x => x.DriverInformations);
            builder.HasOne(x => x.CreatedByNavigation).WithMany(x => x.DriverInformationCreatedByNavigations).HasForeignKey(x => x.CreatedBy);
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.DriverInformationUpdatedByNavigations).HasForeignKey(x => x.UpdatedBy);
        }
    }
}
