using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("(0)");
            
            builder.HasOne(x => x.UserGarage).WithMany(x => x.Attachments);
        }
    }
}
