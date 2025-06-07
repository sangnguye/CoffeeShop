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

        public OrdersController(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this._orderRepository = orderRepository;
            this._shoppingCartRepository = shoppingCartRepository;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Order order)
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

            return RedirectToAction("CheckoutComplete");
        }



        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}
