using DevCars.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Api.Persistence.Configurations
{
    public class ExtraOrderItemDbConfiguration : IEntityTypeConfiguration<ExtraOrderItem>
    {
        public void Configure(EntityTypeBuilder<ExtraOrderItem> builder)
        {
            builder.HasKey(c => c.Id);
            //builder.ToTable("ExtraOrderItem");
        }
    }
}
