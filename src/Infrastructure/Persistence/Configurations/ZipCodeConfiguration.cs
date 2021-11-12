using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ZipCodeConfiguration : IEntityTypeConfiguration<ZipCode>
    {
        public void Configure(EntityTypeBuilder<ZipCode> builder)
        {
            builder.ToTable("ZipCode");
            builder.Property(x => x.Code).HasMaxLength(16).IsRequired();

            builder.HasOne(x => x.City).WithMany(x => x.ZipCodes);
            
        }
    }
}
