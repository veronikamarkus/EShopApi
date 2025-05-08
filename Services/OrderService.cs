using AutoMapper;
using EShopApi.Cache;
using EShopApi.DTOs.Orders;
using EShopApi.Models;
using EShopApi.Repositories;

namespace EShopApi.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _ordersRepo;

    private readonly IProductRepository _productsRepo;

    private readonly IMapper _mapper;

    private readonly ICacheService _cache;


    public OrderService(IOrderRepository ordersRepo, IProductRepository productsRepo, IMapper mapper, ICacheService cache) 
    {
        _ordersRepo = ordersRepo;
        _productsRepo = productsRepo;
        _mapper = mapper;
        _cache = cache;
    }


    public async Task<IEnumerable<OrderReadDTO>> GetAllAsync()
    {
        var cached = await _cache.GetAsync<List<OrderReadDTO>>(CacheKeys.ORDERS_ALL);

        if (cached != null)
            return cached;

        var orders = await _ordersRepo.GetAllAsync();
        var orderDTOs = _mapper.Map<IEnumerable<OrderReadDTO>>(orders);

        await _cache.SetAsync(CacheKeys.ORDERS_ALL, orderDTOs, TimeSpan.FromMinutes(1));
        
        return orderDTOs;
    }

    public async Task<OrderReadDTO?> GetByIdAsync(Guid id)
    {
        var order = await _ordersRepo.GetByIdAsync(id);

        return order == null ? null : _mapper.Map<OrderReadDTO>(order);
    }

    public async Task<OrderReadDTO> CreateAsync(OrderCreateDTO request)
    {
        var order = new Order();

        foreach (var item in request.Items)
        {
            var product = await _productsRepo.GetByIdAsync(item.ProductId)
                ?? throw new ArgumentException($"Product not found: {item.ProductId}");

            order.OrderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });
        }

        await _ordersRepo.AddAsync(order);

        await _cache.RemoveAsync(CacheKeys.ORDERS_ALL);

        return _mapper.Map<OrderReadDTO>(order);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _ordersRepo.DeleteAsync(id);

        await _cache.RemoveAsync(CacheKeys.ORDERS_ALL);
    }

}
