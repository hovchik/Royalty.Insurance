using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SavedMarketingRequestConfiguration : IEntityTypeConfiguration<SavedMarketingRequest>
    {
        public void Configure(EntityTypeBuilder<SavedMarketingRequest> builder)
        {
            builder.ToTable("SavedMarketingRequests");
            builder.Property(x => x.SavedRequest).HasColumnType("text").IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.SavedMarketingRequests);
        }
    }
}
