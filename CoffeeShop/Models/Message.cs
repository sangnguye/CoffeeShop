namespace CoffeeShop.Models
{
    public class Message
    {
        public int Id { get; set; }  // Khóa chính cho EF
        public string Name { get; set; } = string.Empty;  // Tên người gửi
        public string Email { get; set; } = string.Empty; // Email người gửi
        public string Content { get; set; } = string.Empty; // Nội dung tin nhắn
        public DateTime CreatedAt { get; set; } = DateTime.Now;  // Thời gian gửi (nên có)
    }
}
