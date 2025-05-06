using EShopApi.DTOs.OrderItems;

namespace EShopApi.DTOs.Orders 
{
    public class OrderCreateDTO
    {
        public List<OrderItemCreateDTO> Items { get; set; } = [];
    }
}

