using DevCars.Api.Entities;
using DevCars.Api.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DevCars.Api.Persistence
{
    public class DevCarsDbContext : DbContext
    {
        public DevCarsDbContext(DbContextOptions<DevCarsDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraOrderItem> ExtraOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CarDbConfiguration());
            //modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());
            //modelBuilder.ApplyConfiguration(new OrderDbConfiguration());
            //modelBuilder.ApplyConfiguration(new ExtraOrderItemDbConfiguration());

            //tudo aquilo em cima é igual a essa linha
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
