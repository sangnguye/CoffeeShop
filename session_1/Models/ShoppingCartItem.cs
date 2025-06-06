using session_1.Models;

namespace session_1.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Product? Products { get; set; }
        public int Qty { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}