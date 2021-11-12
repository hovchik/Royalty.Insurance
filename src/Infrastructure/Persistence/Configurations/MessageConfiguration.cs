using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.Property(x => x.Body).HasMaxLength(1024);
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

            builder.HasOne(x => x.Sender).WithMany(x => x.Messages);
            builder.HasOne(x => x.RecipientGroup).WithMany(x => x.Messages);
            builder.HasOne(x => x.Parent).WithMany(x => x.InverseParent);
        }
    }
    
}
