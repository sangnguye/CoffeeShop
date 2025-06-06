using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using session_1.Models;

namespace session_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Tạo danh sách mẫu Product
            var product = new List<Product>
            {
                new Product { Name = "Espresso", Price = 4.50m, ImageUrl = "https://file.hstatic.net/200000886877/article/ca-phe-espresso-1_f71dd8f8b25b4f92889956e66a636be6.jpg" },
                new Product { Name = "Latte", Price = 3.50m, ImageUrl = "https://vinbarista.com/uploads/news/ca-phe-latte-la-gi-latte-co-vi-gi-latte-khac-gi-capuchino-202408161122.jpg " },
                new Product { Name = "Cappuccino", Price = 3.00m, ImageUrl = "https://images.unsplash.com/photo-1541167760496-1628856ab772?ixlib=rb-4.0.3&auto=format&fit=crop&w=300&q=80" }
            };

            return View(product); // Truyền danh sách vào view....
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