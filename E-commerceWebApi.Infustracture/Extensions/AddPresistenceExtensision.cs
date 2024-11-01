

using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using E_commerceWebApi.Infustracture.Data;
using E_commerceWebApi.Infustracture.Email;
using E_commerceWebApi.Infustracture.Implementation;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace E_commerceWebApi.Infustracture.Extensions
{
    public static class AddPresistenceExtensision
    {
        public static IServiceCollection AddPresistence(this IServiceCollection Repository, IConfiguration configuration)
        {

            Repository.AddScoped<IUserRepository, UserRepository>();
            Repository.AddScoped<IImageRepository, ImageRepository>();
            Repository.AddScoped<ICategoryRepository, CategoryRepository>();
            Repository.AddScoped<ISubcategoryRepository, SubCategoryRepository>();
            Repository.AddScoped<IProductRepository, ProductRepository>();
            Repository.AddScoped<IColorRepository, ColorRepository>();
            Repository.AddScoped<ISizeRepository, SizeRepository>();
            Repository.AddScoped<IProductColorsRepository, ProductColorsRepository>();
            Repository.AddScoped<IProductSizesRepository, ProductSizesRepository>();
            Repository.AddScoped<IProductImagesRepository, ProductImagesRepository>();
            Repository.AddScoped<ICartRepository, CartRepository>();
            Repository.AddScoped<IOrderRepository, OrderRepository>();
            Repository.AddScoped<ICategoryRepository, CategoryRepository>();
            Repository.AddScoped<ICouponRepository, CouponRepository>();
            Repository.AddScoped<ITokenRepository, TokenRepository>();
            Repository.AddScoped<IUnitofWork, UnitofWork>();
            Repository.AddScoped<IEmailSender, EmailSender>();

            Repository.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionstring"));
            });
            Repository.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            
            return Repository;
        }
    }
}
