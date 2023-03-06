using KitchenRoutingSystemPOS.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRoutingSystemPOS.Data
{
    public class KitchenContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Kitchen");
        }
    }
}
