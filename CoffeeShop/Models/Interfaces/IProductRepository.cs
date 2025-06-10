using System.Reflection.Metadata.Ecma335;

namespace CoffeShop.Models.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Products>  GetAllProducts();
        public IEnumerable<Products> GetTrendingProducts();
        public Products GetProductDetail(int id);

        void Add(Products product);
        void Update(Products product);
        void Delete(int id);
        void Save();
    }
}
