using System.Diagnostics;
using CoffeShop.Models;
using CoffeShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeShop.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository ProductRepository;
        public HomeController(IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
        }


        public IActionResult Index()
        {
            return View(ProductRepository.GetTrendingProducts());
        }
        public IActionResult Shop()
        {
            return View(ProductRepository.GetAllProducts());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
