using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class InsuredVehicleConfiguration : IEntityTypeConfiguration<InsuredVehicle>
    {
        public void Configure(EntityTypeBuilder<InsuredVehicle> builder)
        {
            builder.ToTable("InsuredVehicle");
            builder.HasOne(x => x.Insured).WithMany(x => x.InsuredVehicles);
            builder.HasOne(x => x.Vehicle).WithMany(x => x.InsuredVehicles);
        }
    }
}
