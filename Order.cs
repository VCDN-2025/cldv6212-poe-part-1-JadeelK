namespace Way2GoApp.Models
{
    public class Order
    {
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
    }
}