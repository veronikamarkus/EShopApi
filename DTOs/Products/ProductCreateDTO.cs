namespace EShopApi.DTOs.Products
{
     public class ProductCreateDTO
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}