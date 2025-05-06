namespace EShopApi.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; } = default!;

        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}