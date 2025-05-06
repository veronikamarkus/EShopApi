using EShopApi.DTOs.OrderItems;

namespace EShopApi.DTOs.Orders
{
    public class OrderReadDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemReadDTO> OrderItems { get; set; } = [];
    }
}
