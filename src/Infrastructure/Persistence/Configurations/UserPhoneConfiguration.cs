using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserPhoneConfiguration : IEntityTypeConfiguration<UserPhone>
    {
        public void Configure(EntityTypeBuilder<UserPhone> builder)
        {
            builder.ToTable("UserPhones");
            builder.Property(x => x.PhoneNumber).HasMaxLength(15).IsRequired();
            builder.Property(x => x.IpAddress).HasMaxLength(15).IsRequired();
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");


            builder.HasOne(x => x.CreatedByNavigation).WithMany(x => x.UserPhoneCreatedByNavigations);
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.UserPhoneUpdatedByNavigations);
        }
    }
}