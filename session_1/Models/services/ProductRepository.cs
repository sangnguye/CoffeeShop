using session_1.Data;
using session_1.Models;
using session_1.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace session_1.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly CoffeeShopDbContext _dbContext;

        public ProductRepository(CoffeeShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products;
        }

        public Product? GetProductDetail(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetTrendingProducts()
        {
            return _dbContext.Products.Where(p => p.IsTrendingProduct);
        }
    }
}