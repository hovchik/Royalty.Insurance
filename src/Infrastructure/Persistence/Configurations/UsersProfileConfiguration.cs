using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UsersProfileConfiguration : IEntityTypeConfiguration<UsersProfile>
    {
        public void Configure(EntityTypeBuilder<UsersProfile> builder)
        {
            builder.ToTable("UsersProfiles");

            builder.HasOne(d => d.User)
                .WithOne(p => p.UsersProfile)
                .HasForeignKey<UsersProfile>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersProfiles_Users");
            builder.Property(x => x.UserLastStatusId).HasDefaultValueSql("(2)");

            builder.HasOne(x => x.UserLastStatus).WithMany(x => x.UsersProfileUserLastStatuses);
                    builder.HasOne(x => x.UserStatus).WithMany(x => x.UsersProfileUserStatuses);
        }
    }
}
