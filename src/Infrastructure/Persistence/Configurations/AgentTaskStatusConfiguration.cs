using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class AgentTaskStatusConfiguration : IEntityTypeConfiguration<AgentTaskStatus>
    {
        public void Configure(EntityTypeBuilder<AgentTaskStatus> builder)
        {
            builder.ToTable("AgentTaskStatuses");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
