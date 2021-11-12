using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserPhoneCallHistoryConfiguration : IEntityTypeConfiguration<UserPhoneCallHistory>
    {
        public void Configure(EntityTypeBuilder<UserPhoneCallHistory> builder)
        {
            builder.ToTable("UserPhoneCallHistory");
            builder.Property(x => x.CallerNumber).HasMaxLength(15).IsRequired();
            builder.Property(x => x.CallId).HasMaxLength(15).IsRequired();
            builder.Property(x => x.CallerName).HasMaxLength(50);
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

            builder.HasOne(x => x.UserPhone).WithMany(x => x.UserPhoneCallHistories).HasForeignKey(x => x.UserPhoneId);
            builder.HasOne(x => x.CurrentCallType).WithMany(x => x.UserPhoneCallHistoryCurrentCallTypes);
        }
    }
}
