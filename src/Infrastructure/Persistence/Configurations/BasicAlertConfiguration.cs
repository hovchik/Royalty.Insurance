using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class BasicAlertConfiguration : IEntityTypeConfiguration<BasicAlert>
    {
        public void Configure(EntityTypeBuilder<BasicAlert> builder)
        {
            builder.ToTable("BasicAlert");
            builder.Property(x => x.BasicAlert1).HasMaxLength(30).IsRequired();
        }
    }
}
