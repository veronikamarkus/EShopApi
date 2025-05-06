using AutoMapper;
using EShopApi.DTOs;
using EShopApi.DTOs.Orders;
using EShopApi.Models;
using EShopApi.Repositories;

namespace EShopApi.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _ordersRepo;

    private readonly IProductRepository _productsRepo;

    private readonly IMapper _mapper;

    public OrderService(IOrderRepository ordersRepo, IProductRepository productsRepo, IMapper mapper) 
    {
        _ordersRepo = ordersRepo;
        _productsRepo = productsRepo;
        _mapper = mapper;
    }


    public async Task<IEnumerable<OrderReadDTO>> GetAllAsync()
    {
        var orders = await _ordersRepo.GetAllAsync();
        
        return _mapper.Map<IEnumerable<OrderReadDTO>>(orders);
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

        return _mapper.Map<OrderReadDTO>(order);
    }

    public Task DeleteAsync(Guid id) => _ordersRepo.DeleteAsync(id);

}
