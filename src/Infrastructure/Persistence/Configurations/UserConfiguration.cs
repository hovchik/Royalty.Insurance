using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(256);
            builder.Property(x => x.PersonalAvatar).HasMaxLength(255);
            builder.Property(x => x.ForgetPasswordCode).HasMaxLength(6);
            builder.Property(x => x.HomePhone).HasMaxLength(15);
            builder.Property(x => x.CellPhone).HasMaxLength(15);
            builder.Property(x => x.WorkPhone).HasMaxLength(15);
            builder.Property(x => x.AdditionalPhone).HasMaxLength(15);
            builder.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.Password).IsFixedLength(true);
            builder.Property(e => e.Salting).IsFixedLength(true);
            builder.Property(e => e.TemporaryPassword).HasDefaultValueSql("((1))");
            builder.Property(x => x.IsBlocked).HasDefaultValueSql("(0)");
            builder.Property(x => x.FailedLoginCount).HasDefaultValueSql("(0)");
        }
    }
}
