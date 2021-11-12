using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class LossInformationConfiguration : IEntityTypeConfiguration<LossInformation>

    {
        public void Configure(EntityTypeBuilder<LossInformation> builder)
        {
            builder.ToTable("LossInformation");
            builder.Property(x => x.InsuranceName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LesseeName).HasMaxLength(50);
            builder.Property(x => x.PoliceNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LesseeMcnumber).HasMaxLength(50).HasColumnName("LesseeMCNumber");
            builder.Property(x => x.NumberOfClaims).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Comments).HasColumnType("ntext");
        }
    }
}
