using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class LegalStatusConfiguration : IEntityTypeConfiguration<LegalStatus>
    {
        public void Configure(EntityTypeBuilder<LegalStatus> builder)
        {
            builder.ToTable("LegalStatuses");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
