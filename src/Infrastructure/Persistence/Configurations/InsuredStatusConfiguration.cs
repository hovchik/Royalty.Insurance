using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class InsuredStatusConfiguration : IEntityTypeConfiguration<InsuredStatus>
    {
        public void Configure(EntityTypeBuilder<InsuredStatus> builder)
        {
            builder.ToTable("InsuredStatuses");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
