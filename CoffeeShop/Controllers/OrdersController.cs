using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoffeeShop.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderRepository _orderRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        private CoffeeshopDbContext _context;

        public OrdersController(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository,
        CoffeeshopDbContext context
        )
        {
            this._orderRepository = orderRepository;
            this._shoppingCartRepository = shoppingCartRepository;
            this._context = context;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CheckoutAsync(Order order)
        {
            var cartItems = _shoppingCartRepository.GetAllShoppingCartItems();

            // Gán email người dùng đang đăng nhập
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }
            order.Email = email;

            // Gán chi tiết đơn hàng
            order.OrderDetails = cartItems.Select(item => new OrderDetail
            {
                ProductId = item.Products.Id,
                Quantity = item.Qty,
                Price = item.Products.Price,
            }).ToList();

            order.OrderTotal = cartItems.Sum(item => item.Products.Price * item.Qty);
            order.OrderPlaced = DateTime.Now;

            _orderRepository.PlaceOrder(order);

            _shoppingCartRepository.ClearCart();
            HttpContext.Session.SetInt32("CartCount", 0);
            // Sau khi lưu đơn hàng
            _context.Notifications.Add(new Notification
            {
                Title = $"Đơn hàng mới #{order.Id}",
                Url = $"/Admin/Orders/Details/{order.Id}",
                IsRead = false,
                CreatedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();  // Lưu notification sau



            return RedirectToAction("CheckoutComplete");
        }



        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}