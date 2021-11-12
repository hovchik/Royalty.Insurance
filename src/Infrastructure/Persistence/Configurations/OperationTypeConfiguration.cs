using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationType>
    {
        public void Configure(EntityTypeBuilder<OperationType> builder)
        {
            builder.ToTable("OperationType");
            builder.Property(x => x.Type).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Value).HasMaxLength(15).IsRequired();
        }
    }
}
