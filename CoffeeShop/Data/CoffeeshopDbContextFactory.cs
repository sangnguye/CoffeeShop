using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoffeeShop.Data
{
    public class CoffeeshopDbContextFactory : IDesignTimeDbContextFactory<CoffeeshopDbContext>
    {
        public CoffeeshopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoffeeshopDbContext>();

            optionsBuilder.UseSqlServer("Server=ASUS_VIVOBOOK;Database=CoffeeShopDBnew;User Id=sa;Password=i990c,b7;Encrypt=False;TrustServerCertificate=True;Pooling=True;");

            return new CoffeeshopDbContext(optionsBuilder.Options);
        }
    }
}
