using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class AgentTaskConfiguration : IEntityTypeConfiguration<AgentTask>
    {
        public void Configure(EntityTypeBuilder<AgentTask> builder)
        {
            builder.ToTable("AgentTasks");
            builder.Property(x => x.Title).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1024);
            builder.Property(x => x.CanceledReason).HasMaxLength(255);
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

            builder.HasOne(x => x.AgentTaskStatus).WithMany(x => x.AgentTasks);
            builder.HasOne(x => x.AgentTaskType).WithMany(x => x.AgentTasks);
            builder.HasOne(x => x.CreatedByNavigation).WithMany(x => x.AgentTaskCreatedByNavigations).HasForeignKey(x => x.CreatedBy);
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.AgentTaskUpdatedByNavigations).HasForeignKey(x => x.UpdatedBy);
            builder.HasOne(x => x.Assignee).WithMany(x => x.AgentTaskAssignees);
        }
    }
}
