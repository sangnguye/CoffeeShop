using CoffeShop.Models;

namespace CoffeeShop.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public Products? Product { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
