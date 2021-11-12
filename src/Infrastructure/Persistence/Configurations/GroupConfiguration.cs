using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.Active).HasDefaultValueSql("(1)");

            builder.HasOne(x => x.CreatedByNavigation).WithMany(x => x.GroupCreatedByNavigations).HasForeignKey(x => x.CreatedBy);
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.GroupUpdatedByNavigations).HasForeignKey(x => x.UpdatedBy);
            builder.HasMany(x => x.GroupMembers).WithOne(x => x.Group).HasForeignKey(x => x.GroupId);
        }
    }
}
