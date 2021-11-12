using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.Property(x => x.DocumentName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Path).HasMaxLength(1024).IsRequired();
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("(0)");

            builder.HasOne(x => x.DocumentType).WithMany(x => x.Documents);
            builder.HasOne(x => x.CreatedByNavigation).WithMany(x => x.DocumentCreatedByNavigations).HasForeignKey(x => x.CreatedBy);
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.DocumentUpdatedByNavigations).HasForeignKey(x => x.UpdatedBy);
        }
    }
}
