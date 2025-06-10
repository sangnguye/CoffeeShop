
using CoffeeShop.Data;
using CoffeShop.Models;
using CoffeShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace coffeeshop.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private CoffeeshopDbContext dbContext;
        public ProductRepository(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return dbContext.Products.ToList();
        }

        public Products? GetProductDetail(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Products> GetTrendingProducts()
        {
            return dbContext.Products.Where(p => p.IsTrendingProduct);
        }

        public void Add(Products product)
        {
            dbContext.Products.Add(product);
        }

        public void Update(Products product)
        {
            dbContext.Products.Update(product);
        }

        public void Delete(int id)
        {
            var product = dbContext.Products.Find(id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}