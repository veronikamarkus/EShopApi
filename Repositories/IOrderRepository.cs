using EShopApi.Models;

namespace EShopApi.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();

    Task<Order?> GetByIdAsync(Guid id);

    Task AddAsync(Order Order);

    Task DeleteAsync(Guid id);
}
