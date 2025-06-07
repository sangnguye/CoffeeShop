using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoffeeShop.Controllers
{
    [Authorize] // đảm bảo chỉ người đăng nhập mới truy cập
    public class ListOrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult ListOrder()
        {
            // Lấy email từ người dùng đang đăng nhập
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Truy vấn các đơn hàng của người dùng
            var orders = _orderRepository.GetOrdersByEmail(email);

            return View(orders);
        }
    }
}
