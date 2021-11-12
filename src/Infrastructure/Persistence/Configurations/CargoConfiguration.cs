using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.ToTable("Cargo");
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.CargoUpdatedByNavigations).HasForeignKey(x => x.UpdatedBy);
            builder.HasOne(x => x.CreateByNavigation).WithMany(x => x.CargoCreateByNavigations).HasForeignKey(x => x.CreateBy);
            builder.HasOne(x => x.Insured).WithMany(x => x.Cargos);
        }
    }
}
