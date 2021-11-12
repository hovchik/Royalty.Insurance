using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");
            builder.Property(x => x.Description).HasMaxLength(256).IsRequired();
            builder.Property(e => e.CreateDateTime).HasDefaultValueSql("(getutcdate())");

            //todo: insured
            builder.HasOne(x => x.User).WithMany(x => x.Notes);
        }
    }
}
