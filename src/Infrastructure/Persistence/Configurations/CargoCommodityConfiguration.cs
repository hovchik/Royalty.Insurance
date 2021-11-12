using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CargoCommodityConfiguration : IEntityTypeConfiguration<CargoCommodity>
    {
        public void Configure(EntityTypeBuilder<CargoCommodity> builder)
        {
            builder.ToTable("CargoCommodity");
            builder.HasOne(x => x.Commodity).WithMany(x => x.CargoCommodities);
            builder.HasOne(x => x.Cargo).WithMany(x => x.CargoCommodities);
        }
    }
}
