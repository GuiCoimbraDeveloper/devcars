using DevCars.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Api.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            //builder.ToTable("Car");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Brand).HasMaxLength(100);
            builder.Property(c => c.Brand).HasColumnType("VARCHAR(100)");
            builder.Property(c => c.Brand).HasDefaultValue("PADRÃO");

            builder.Property(c => c.ProductionDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
