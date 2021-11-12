using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class AgaveTransactionTypeConfiguration : IEntityTypeConfiguration<AgaveTransactionType>
    {
        public void Configure(EntityTypeBuilder<AgaveTransactionType> builder)
        {
            builder.ToTable("AgaveTransactionType");
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();
        }
    }
}
