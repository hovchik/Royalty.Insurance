using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class DatabaseVersionConfiguration : IEntityTypeConfiguration<DatabaseVersion>
    {
        public void Configure(EntityTypeBuilder<DatabaseVersion> builder)
        {
            builder.ToTable("_DatabaseVersion");
            builder.Property(x => x.DbVersion).HasMaxLength(16).IsRequired();
        }
    }
}
