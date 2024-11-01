using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace E_commerceWebApi.Api.Configuration
{
    public static class ConfiqurationJWTServices
    {
        public static IServiceCollection AddJWTServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSwaggerGen(options =>
             {
                options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter 'Bearer' followed by a space and your token.",
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new string[] { }
                    }   
                });
             });

                // Identity services
                services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("E-Commerce")
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

                services.Configure<IdentityOptions>(options =>
                 {
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                 });

            // JWT Authentication
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["JwtOptions:Issuer"],
                            ValidAudience = configuration["JwtOptions:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Secret"]))
                        };
                    });

            return services;
        }
    }
}
