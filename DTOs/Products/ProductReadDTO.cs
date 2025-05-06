namespace EShopApi.DTOs.Products
{
     public class ProductReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }

    }
}