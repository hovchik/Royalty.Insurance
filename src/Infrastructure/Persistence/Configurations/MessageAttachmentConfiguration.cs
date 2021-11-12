using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MessageAttachmentConfiguration : IEntityTypeConfiguration<MessageAttachment>
    {
        public void Configure(EntityTypeBuilder<MessageAttachment> builder)
        {
            builder.ToTable("MessageAttachments");
            builder.HasKey(x => new { x.MessageId, x.AttachmentId });
            builder.HasOne(x => x.Message).WithMany(x => x.MessageAttachments);
            builder.HasOne(x => x.Attachment).WithMany(x => x.MessageAttachments);
        }
    }
}
