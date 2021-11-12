using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class CallTypeConfiguration : IEntityTypeConfiguration<CallType>
    {
        public void Configure(EntityTypeBuilder<CallType> builder)
        {
            builder.ToTable("CallTypes");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
