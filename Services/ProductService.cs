using AutoMapper;
using EShopApi.Cache;
using EShopApi.DTOs.Products;
using EShopApi.Models;
using EShopApi.Repositories;

namespace EShopApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, ICacheService cache, IMapper mapper)
    {
        _repo = repo;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductReadDTO>> GetAllAsync()
    {
        var cached = await _cache.GetAsync<List<ProductReadDTO>>(CacheKeys.PRODUCTS_ALL);

        if (cached != null)
            return cached;

        var products = await _repo.GetAllAsync();
        var productDTOs = _mapper.Map<List<ProductReadDTO>>(products);

        await _cache.SetAsync(CacheKeys.PRODUCTS_ALL, productDTOs, TimeSpan.FromMinutes(1));

        return productDTOs;
    }

    public async Task<ProductReadDTO?> GetByIdAsync(Guid id)
    {
        string cacheKey = $"product_{id}";
        var cached = await _cache.GetAsync<ProductReadDTO>(cacheKey);
        if (cached != null) return cached;

        var product = await _repo.GetByIdAsync(id) ?? throw new ArgumentException("Product not found");
        var ProductReadDto = _mapper.Map<ProductReadDTO>(product);

        await _cache.SetAsync(cacheKey, ProductReadDto, TimeSpan.FromMinutes(1));

        return product == null ? null : ProductReadDto;
    }

    public async Task<ProductReadDTO> AddAsync(ProductCreateDTO dto)
    {
        var product = _mapper.Map<Product>(dto);

        await _repo.AddAsync(product);

        await _cache.RemoveAsync(CacheKeys.PRODUCTS_ALL);

        return _mapper.Map<ProductReadDTO>(product);
    }

    public async Task UpdateAsync(Guid id, ProductUpdateDTO dto)
    {
        var product = await _repo.GetByIdAsync(id) ?? throw new ArgumentException("Product not found");

        _mapper.Map(dto, product);

        await _repo.UpdateAsync(product);

        await _cache.RemoveAsync($"product_{id}");
        await _cache.RemoveAsync(CacheKeys.PRODUCTS_ALL);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repo.DeleteAsync(id);

        await _cache.RemoveAsync($"product_{id}");
        await _cache.RemoveAsync(CacheKeys.PRODUCTS_ALL);
    }
}
