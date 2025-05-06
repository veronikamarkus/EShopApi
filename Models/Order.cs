namespace EShopApi.Models
{
    public class Order : BaseModel
    {     
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        
    }
}
