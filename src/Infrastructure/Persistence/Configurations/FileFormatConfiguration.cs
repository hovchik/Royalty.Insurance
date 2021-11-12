using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class FileFormatConfiguration : IEntityTypeConfiguration<FileFormat>
    {
        public void Configure(EntityTypeBuilder<FileFormat> builder)
        {
            builder.ToTable("FileFormats");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CodeType).HasMaxLength(50).IsRequired();
        }
    }
}
