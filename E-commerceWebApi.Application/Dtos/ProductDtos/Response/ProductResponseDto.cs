using E_commerceWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.ProductDtos.Response
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string EnglishName { get; set; } = string.Empty;
        public string ArabicName { get; set; } = string.Empty;
        public string EnglishDescription { get; set; } = string.Empty;
        public string ArabicDescription { get; set; } = string.Empty;
        public float Price { get; set; }
        public float DiscountPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public bool IsFeatured { get; set; }
        public int SubcategoryId { get; set; }


        public Product ToModel(ProductResponseDto product)
        {
            return new Product
            {
                Id = product.Id,
                EnglishName = product.EnglishName,
                EnglishDescription = product.EnglishDescription,
                ArabicDescription = product.ArabicDescription,
                ArabicName = product.ArabicName,
                DiscountPrice = product.DiscountPrice,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                IsFeatured = product.IsFeatured,
                SubcategoryId = product.SubcategoryId,
                Price = product.Price,
            };
        }

        public ProductResponseDto ToResponse(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                EnglishName = product.EnglishName,
                EnglishDescription = product.EnglishDescription,
                ArabicDescription = product.ArabicDescription,
                ArabicName = product.ArabicName,
                DiscountPrice = product.DiscountPrice,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                IsFeatured = product.IsFeatured,
                SubcategoryId = product.SubcategoryId,
                Price = product.Price,
            };
        }
    }
}
