namespace EShopApi.DTOs.OrderItems
{
    public class OrderItemCreateDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}