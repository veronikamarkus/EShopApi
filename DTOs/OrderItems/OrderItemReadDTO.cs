namespace EShopApi.DTOs.OrderItems
{
    public class OrderItemReadDTO
    {
        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}