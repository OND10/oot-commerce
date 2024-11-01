

using E_commerceWebApi.Application.Services.Carts.Implementation;
using E_commerceWebApi.Application.Services.Carts.Interface;
using E_commerceWebApi.Application.Services.CartsItems.Interface;
using E_commerceWebApi.Application.Services.Categories.Implementation;
using E_commerceWebApi.Application.Services.Categories.Interface;
using E_commerceWebApi.Application.Services.Colors.Implementation;
using E_commerceWebApi.Application.Services.Colors.Interface;
using E_commerceWebApi.Application.Services.Coupons.Implementation;
using E_commerceWebApi.Application.Services.Coupons.Interface;
using E_commerceWebApi.Application.Services.Image.implementation;
using E_commerceWebApi.Application.Services.Image.Interface;
using E_commerceWebApi.Application.Services.Orders.Implementation;
using E_commerceWebApi.Application.Services.Orders.Interface;
using E_commerceWebApi.Application.Services.Products.Implementation;
using E_commerceWebApi.Application.Services.Products.Interface;
using E_commerceWebApi.Application.Services.ProductsColors.Implementation;
using E_commerceWebApi.Application.Services.ProductsColors.Interface;
using E_commerceWebApi.Application.Services.ProductsImages.Implementation;
using E_commerceWebApi.Application.Services.ProductsImages.Interface;
using E_commerceWebApi.Application.Services.ProductsSizes.Implementation;
using E_commerceWebApi.Application.Services.ProductsSizes.Interface;
using E_commerceWebApi.Application.Services.Sizes.Implementation;
using E_commerceWebApi.Application.Services.Sizes.Interface;
using E_commerceWebApi.Application.Services.SubCategories.Implementation;
using E_commerceWebApi.Application.Services.SubCategories.Interface;
using E_commerceWebApi.Application.Services.User.Implementation;
using E_commerceWebApi.Application.Services.User.Interface;
using E_commerceWebApi.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using OnMapper;

namespace E_commerceWebApi.Application.Extensions
{
    public static class AddApplicationExtensision
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {

            service.AddScoped<OnMapping>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IImageService, ImageService>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<ISubCategoryService, SubCategoryService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IColorService, ColorService>();
            service.AddScoped<ISizeService, SizeService>();
            service.AddScoped<IProductColorsService, ProductColorsService>();
            service.AddScoped<IProductSizesService, ProductSizesService>();
            service.AddScoped<IProductImagesService, ProductImagesService>();
            service.AddScoped<ICartService, CartService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<ICouponService, CouponService>();
            

            return service;
        }
    }
}
