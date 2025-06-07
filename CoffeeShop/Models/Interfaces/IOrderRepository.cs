namespace CoffeeShop.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrdersByEmail(string email);
        void PlaceOrder(Order order);
    }
}
