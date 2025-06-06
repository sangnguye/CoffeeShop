using Microsoft.AspNetCore.Mvc;
using session_1.Models;
using session_1.Models.Interfaces;

namespace session_1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Shop()
        {
            var products = productRepository.GetAllProducts();
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}