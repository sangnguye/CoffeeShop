// Controllers/AdminController.cs
using coffeeshop.Models.Services;
using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Models.Interfaces;
using CoffeShop.Models;
using CoffeShop.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;



public class AdminController : Controller
{
    private readonly CoffeeshopDbContext _context;
    private IProductRepository productRepository;
    private readonly IOrderRepository orderRepository;
    private readonly UserManager<IdentityUser> _userManager;
    public AdminController(IProductRepository productRepository, IOrderRepository orderRepository, UserManager<IdentityUser> userManager, CoffeeshopDbContext _context)
    {
        this.productRepository = productRepository;
        this.orderRepository = orderRepository;
        this._userManager = userManager;
        this._context = _context;
    }
  
    


    [Route("Admin")]
    public ActionResult Index()
    {
        ViewBag.TotalUsers = _userManager.Users.Count(); // ✅ Đúng
        ViewBag.TotalProducts = productRepository.GetAllProducts().Count(); // ✅ Đếm từ repository
        ViewBag.TotalOrders = orderRepository.GetAllOrders().Count();

        return View();
    }

    [Route("Admin/Products")]
    public ActionResult Products()
    {
        var products = productRepository.GetAllProducts();
        return View("~/Views/Admin/Product/Products.cshtml", products);
    }

    [HttpPost]
    public IActionResult CreateProduct(Products product)
    {
        if (ModelState.IsValid)
        {
            productRepository.Add(product);
            productRepository.Save();
            return RedirectToAction("Products");
        }
        return View("~/Views/Admin/Product/CreateProduct.cshtml", product);
    }
    public IActionResult EditProduct(int id)
    {
        var product = productRepository.GetProductDetail(id);
        if (product == null)
            return NotFound();
        return View("~/Views/Admin/Product/EditProduct.cshtml", product);
    }

    [HttpPost]
    public IActionResult EditProduct(Products product)
    {
        if (ModelState.IsValid)
        {
            productRepository.Update(product);
            productRepository.Save();
            return RedirectToAction("Products");
        }
        return View("~/Views/Admin/Product/EditProduct.cshtml", product);
    }

    public IActionResult DeleteProduct(int id)
    {
        var product = productRepository.GetProductDetail(id);
        if (product == null)
            return NotFound();
        return View("~/Views/Admin/Product/DeleteProduct.cshtml", product);
    }

    [HttpPost, ActionName("DeleteProduct")]
    public IActionResult DeleteConfirmed(int id)
    {
        productRepository.Delete(id);
        productRepository.Save();
        return RedirectToAction("Products");
    }
    [Route("Admin/Orders")]
    public IActionResult GetAllOrders()
    {
        var order = orderRepository.GetAllOrders();
        if (order == null || order.Count == 0)
        {
            return NoContent();
        }
        return View("~/Views/Admin/Order/AllOrders.cshtml", order);
    }

    [Route("Admin/Users")]
    public async Task<IActionResult> Users()
    {
        var users = _userManager.Users.ToList(); // Lấy tất cả người dùng (IQueryable)
        return View("~/Views/Admin/User/Users.cshtml", users);
    }

    [Route("Admin/DashBoard")]
    public IActionResult DashBoard()
    {
        var viewModel = new DashboardChartViewModel
        {
            RevenuePerMonth = Enumerable.Range(1, 12)
                .Select(month => _context.Orders
                    .Where(o => o.OrderPlaced.Month == month && o.OrderPlaced.Year == DateTime.Now.Year)
                    .Sum(o => (decimal?)o.OrderTotal) ?? 0).ToList(),

            OrdersPerMonth = Enumerable.Range(1, 12)
                .Select(month => _context.Orders
                    .Where(o => o.OrderPlaced.Month == month && o.OrderPlaced.Year == DateTime.Now.Year)
                    .Count()).ToList(),

            TopProducts = _context.OrderDetails
                .GroupBy(od => od.Product.Name) // Use 'Product.Name' instead of 'ProductName'
                .Select(g => new ProductStatistic
                {
                    Name = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(p => p.Quantity)
                .Take(3)
                .ToList()
        };

        return View("~/Views/Admin/DashBoard/DashBoard.cshtml", viewModel);
    }

    public IActionResult MarkAsRead(int id)
    {
        var noti = _context.Notifications.FirstOrDefault(n => n.Id == id);
        if (noti != null)
        {
            noti.IsRead = true;
            _context.SaveChanges();

            return Redirect(noti.Url ?? "/Admin");
        }

        return RedirectToAction("Index");
    }
}