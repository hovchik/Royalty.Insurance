using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserGarageConfiguration : IEntityTypeConfiguration<UserGarage>
    {
        public void Configure(EntityTypeBuilder<UserGarage> builder)
        {
            builder.ToTable("UserGarages");
            builder.Property(x => x.FileName).HasMaxLength(50);
            builder.Property(x => x.Path).HasMaxLength(1024).IsRequired();
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

            builder.HasOne(x => x.User).WithMany(x => x.UserGarages);
        }
    }
}
