using EShopApi.DTOs;
using EShopApi.DTOs.Orders;
using EShopApi.Models;

namespace EShopApi.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderReadDTO>> GetAllAsync();

    Task<OrderReadDTO?> GetByIdAsync(Guid id);

    Task<OrderReadDTO> CreateAsync(OrderCreateDTO Order);

    Task DeleteAsync(Guid id);
}
