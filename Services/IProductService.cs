using EShopApi.DTOs.Products;
using EShopApi.Models;

namespace EShopApi.Services;

public interface IProductService
{
    Task<IEnumerable<ProductReadDTO>> GetAllAsync();

    Task<ProductReadDTO?> GetByIdAsync(Guid id);

    Task<ProductReadDTO> AddAsync(ProductCreateDTO product);

    Task UpdateAsync(Guid id, ProductUpdateDTO product);

    Task DeleteAsync(Guid id);
}
