using E_commerceWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Infustracture.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }    
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductSizes> ProductSizes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cart & CartItem Configuration
            //modelBuilder.Entity<CartItem>()
            //    .HasOne(ci => ci.Cart)
            //    .WithMany(c => c.CartItems)
            //    .HasForeignKey(ci => ci.CartId);

            // Order & OrderItem Configuration
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // Product & Category Relationship
            modelBuilder.Entity<Subcategory>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(s => s.CategoryId);

            // Product & ProductImages Configuration
            modelBuilder.Entity<ProductImages>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(pi => pi.ProductId);

            // Product & ProductColors Configuration
            modelBuilder.Entity<ProductColors>()
                .HasOne<Color>()
                .WithMany()
                .HasForeignKey(pc => pc.ColorId);

            // Product & ProductSizes Configuration
            modelBuilder.Entity<ProductSizes>()
                .HasOne<Size>()
                .WithMany()
                .HasForeignKey(ps => ps.SizeId);

            // Additional configurations, indices, constraints, etc.
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.EnglishName)
                .IsUnique();

            // Configure other relationships and constraints similarly.

            base.OnModelCreating(modelBuilder);
        }

    }
}
