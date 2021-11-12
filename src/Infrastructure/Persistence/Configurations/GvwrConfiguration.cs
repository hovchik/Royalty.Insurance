using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GvwrConfiguration : IEntityTypeConfiguration<Gvwr>
    {
        public void Configure(EntityTypeBuilder<Gvwr> builder)
        {
            builder.ToTable("Gvwr");
            builder.Property(x => x.ClassDescription).HasMaxLength(70).IsRequired();
        }
    }
}
