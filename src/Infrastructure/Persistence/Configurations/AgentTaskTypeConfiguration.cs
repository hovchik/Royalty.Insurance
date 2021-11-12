using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class AgentTaskTypeConfiguration : IEntityTypeConfiguration<AgentTaskType>
    {
        public void Configure(EntityTypeBuilder<AgentTaskType> builder)
        {
            builder.ToTable("AgentTaskTypes");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
