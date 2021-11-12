using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Persistence.Configurations
{
    public class AgaveSalesHistoryConfiguration : IEntityTypeConfiguration<AgaveSalesHistory>
    {
        public void Configure(EntityTypeBuilder<AgaveSalesHistory> builder)
        {
            builder.ToTable("AgaveSalesHistory");
            builder.Property(t => t.AvsResponseCode)
                .HasMaxLength(500);

            builder.Property(t => t.CardHolderAddress)
                .HasMaxLength(100);

            builder.Property(t => t.CardHolderCity)
                .HasMaxLength(100);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.AgaveSalesHistories);

            builder.HasOne(x => x.TransactionType)
                   .WithMany(x => x.AgaveSalesHistories);
        }
    }
}
