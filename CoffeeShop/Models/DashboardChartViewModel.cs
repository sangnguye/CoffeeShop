namespace CoffeeShop.Models
{
    public class DashboardChartViewModel
    {
        public List<decimal> RevenuePerMonth { get; set; }
        public List<int> OrdersPerMonth { get; set; }
        public List<ProductStatistic> TopProducts { get; set; }
    }
    public class ProductStatistic
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
