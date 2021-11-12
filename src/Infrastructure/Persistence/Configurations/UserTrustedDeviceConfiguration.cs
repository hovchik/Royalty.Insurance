using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserTrustedDeviceConfiguration : IEntityTypeConfiguration<UserTrustedDevice>
    {
        public void Configure(EntityTypeBuilder<UserTrustedDevice> builder)
        {
            builder.ToTable("UserTrustedDevices");
            builder.Property(x => x.DeviceId).HasMaxLength(128).IsRequired();
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

            builder.HasOne(x => x.User).WithMany(x => x.UserTrustedDevices);
        }
    }
}
