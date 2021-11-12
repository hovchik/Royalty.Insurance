using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class InsuredConfiguration : IEntityTypeConfiguration<Insured>
    {
        public void Configure(EntityTypeBuilder<Insured> builder)
        {
            builder.ToTable("Insureds");

            builder.Property(x => x.SocialSecurityNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.StateNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.MotorCarrierNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Dba).HasMaxLength(100).HasColumnName("DBA");
            builder.Property(x => x.MailingName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.MailingEmail).HasMaxLength(256);
            builder.Property(x => x.MailingStreetAddress).HasMaxLength(256).IsRequired();
            builder.Property(x => x.MailingPhone).HasMaxLength(15);
            builder.Property(x => x.GaragingName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.GaragingStreetAddress).HasMaxLength(256).IsRequired();
            builder.Property(x => x.GaragingPhone).HasMaxLength(15);
            builder.Property(x => x.GaragingName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.GaragingEmail).HasMaxLength(256);
            builder.Property(x => x.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            builder.Property(x => x.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");


            builder.HasOne(x => x.GaragingCity).WithMany(x => x.InsuredGaragingCities).HasForeignKey(x => x.GaragingCityId);
            builder.HasOne(x => x.GaragingState).WithMany(x => x.InsuredGaragingStates).HasForeignKey(x => x.GaragingStateId);
            builder.HasOne(x => x.GaragingZipCode).WithMany(x => x.InsuredGaragingZipCodes).HasForeignKey(x => x.GaragingZipCodeId);
            builder.HasOne(x => x.MailingCity).WithMany(x => x.InsuredMailingCities).HasForeignKey(x => x.MailingCityId);
            builder.HasOne(x => x.MailingState).WithMany(x => x.InsuredMailingStates).HasForeignKey(x => x.MailingStateId);
            builder.HasOne(x => x.MailingZipCode).WithMany(x => x.InsuredMailingZipCodes).HasForeignKey(x => x.MailingZipCodeId);
            builder.HasOne(x => x.InsuredStatus).WithMany(x => x.Insureds).HasForeignKey(x => x.InsuredStatusId);
            builder.HasOne(x => x.LegalStatus).WithMany(x => x.Insureds).HasForeignKey(x => x.LegalStatusId);
            builder.HasOne(x => x.CreateByNavigation).WithMany(x => x.InsuredCreateByNavigations).HasForeignKey(x => x.CreateBy);
            builder.HasOne(x => x.UpdatedByNavigation).WithMany(x => x.InsuredUpdatedByNavigations).HasForeignKey(x => x.UpdatedBy);
        }
    }
}
