using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
    {
        public void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            builder.ToTable("GroupMembers");
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.Active).HasDefaultValueSql("(1)");

            builder.HasOne(x => x.Group).WithMany(x => x.GroupMembers).HasForeignKey(x => x.GroupId);
            builder.HasOne(x => x.Member).WithMany(x => x.GroupMembers).HasForeignKey(x => x.MemberId);
        }
    }
}
