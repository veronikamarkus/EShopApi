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
        const string cacheKey = "products_all";
        var cached = await _cache.GetAsync<List<ProductReadDTO>>(cacheKey);

        if (cached != null)
            return cached;

        var products = await _repo.GetAllAsync();
        var productDTOs = _mapper.Map<List<ProductReadDTO>>(products);

        await _cache.SetAsync(cacheKey, productDTOs, TimeSpan.FromMinutes(1));

        return productDTOs;
    }

    public async Task<ProductReadDTO?> GetByIdAsync(Guid id)
    {
        var product = await _repo.GetByIdAsync(id);

        return product == null ? null : _mapper.Map<ProductReadDTO>(product);
    }

    public async Task<ProductReadDTO> AddAsync(ProductCreateDTO dto)
    {
        var product = _mapper.Map<Product>(dto);

        await _repo.AddAsync(product);

        return _mapper.Map<ProductReadDTO>(product);
    }

    public async Task UpdateAsync(Guid id, ProductUpdateDTO dto)
    {
        var product = await _repo.GetByIdAsync(id) ?? throw new ArgumentException("Product not found");

        _mapper.Map(dto, product);

        await _repo.UpdateAsync(product);
    }

    public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
}
