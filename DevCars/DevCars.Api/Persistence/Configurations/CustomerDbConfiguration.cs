using DevCars.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Api.Persistence.Configurations
{
    public class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            //builder.ToTable("Customer");
            builder.HasMany(a => a.Orders)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
