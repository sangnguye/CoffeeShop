using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data
{
    public class CoffeeshopDbContext : DbContext
    {
        public CoffeeshopDbContext(DbContextOptions<CoffeeshopDbContext>options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected CoffeeshopDbContext()
        {
        }
    }
}
