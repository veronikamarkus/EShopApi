namespace EShopApi.Models
{
    public class OrderItem: BaseModel
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
