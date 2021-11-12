using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CommonAuthTypeConfiguration : IEntityTypeConfiguration<CommonAuthType>
    {
        public void Configure(EntityTypeBuilder<CommonAuthType> builder)
        {
            builder.ToTable("CommonAuthTypes");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();
        }
    }
}
