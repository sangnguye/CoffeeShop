using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models.Services
{
    public class OrderRepository: IOrderRepository
    {
        private readonly CoffeeshopDbContext _context;
        private IShoppingCartRepository _shoppingCartRepository;
        public OrderRepository(CoffeeshopDbContext context, IShoppingCartRepository shoppingCartRepository)
        {
            this._context = context;
            this._shoppingCartRepository = shoppingCartRepository;
        }
        public void PlaceOrder(Order order)
        {
           var shoppingCartItems = _shoppingCartRepository.GetAllShoppingCartItems();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Products.Id,
                    Price = item.Products.Price,
                };
                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _shoppingCartRepository.GetShoppingCartTotal();
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<Order> GetOrdersByEmail(string email)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.Email == email)
                .OrderByDescending(o => o.OrderPlaced)
                .ToList();
        }
    }
}
