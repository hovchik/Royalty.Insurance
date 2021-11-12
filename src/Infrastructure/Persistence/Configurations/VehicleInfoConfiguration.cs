using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class VehicleInfoConfiguration : IEntityTypeConfiguration<VehicleInfo>
    {
        public void Configure(EntityTypeBuilder<VehicleInfo> builder)
        {
            builder.ToTable("VehicleInfo");
            builder.Property(x => x.Make).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Radius).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Vin).HasMaxLength(50).HasColumnName("VIN").IsRequired();
            builder.Property(x => x.Comments).HasColumnType("ntext");
        }
    }
}
