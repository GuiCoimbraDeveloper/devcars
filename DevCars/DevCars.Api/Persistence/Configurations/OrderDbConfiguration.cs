using DevCars.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.Api.Persistence.Configurations
{
    public class OrderDbConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);
            //builder.ToTable("Order");
            builder.HasMany(o => o.ExtraItems).WithOne().HasForeignKey(e => e.IdOrder).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Car).WithOne().HasForeignKey<Order>(o => o.IdCar).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
