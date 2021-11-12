using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UnreadMessageConfiguration : IEntityTypeConfiguration<UnreadMessage>
    {
        public void Configure(EntityTypeBuilder<UnreadMessage> builder)
        {
            builder.ToTable("UnreadMessages");
            builder.Property(x => x.ReadDatetimeUtc).HasDefaultValueSql("(getutcdate())"); 

            builder.HasOne(x => x.Message).WithMany(x => x.UnreadMessages);
            builder.HasOne(x => x.Group).WithMany(x => x.UnreadMessages);
            builder.HasOne(x => x.ReadUser).WithMany(x => x.UnreadMessageReadUsers);
            builder.HasOne(x => x.SendUser).WithMany(x => x.UnreadMessageSendUsers);
        }
    }
}

