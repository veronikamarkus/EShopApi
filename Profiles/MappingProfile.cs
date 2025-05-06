using AutoMapper;
using EShopApi.Models;
using EShopApi.DTOs.Orders;
using EShopApi.DTOs.OrderItems;
using EShopApi.DTOs.Products;

namespace EShopApi.Profiles{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<Order, OrderReadDTO>();

            CreateMap<OrderItem, OrderItemReadDTO>();
            CreateMap<OrderItemCreateDTO, OrderItem>();

            CreateMap<Product, ProductReadDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();
        }
    }
}
