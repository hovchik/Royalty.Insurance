using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserActivityLogConfiguration : IEntityTypeConfiguration<UserActivityLog>
    {
        public void Configure(EntityTypeBuilder<UserActivityLog> builder)
        {
            builder.ToTable("UserActivityLogs");
            builder.Property(x => x.DeviceIp).HasMaxLength(50).IsRequired();
            builder.Property(x => x.RefreshToken).HasMaxLength(64).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.UserActivityLogs);
        }
    }
}
