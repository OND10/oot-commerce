using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductImagesDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.ProductDtos.Response
{
    public class ProductDetailsDto
    {
        public int ProductId { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string EnglishDescription { get; set; }
        public string ArabicDescription { get; set; }
        public float Price { get; set; }
        public float DiscountPrice { get; set; }
        public string MainImageUrl { get; set; } // Main image URL
        public List<ProductImagesResponseDto> Images { get; set; } // Additional images
        public List<ProductColorsResponseDto> Colors { get; set; } // Available colors
        public List<ProductSizesResponseDto> Sizes { get; set; }   // Available sizes
        public int Quantity { get; set; }
        public bool IsFeatured { get; set; }
        public int SubcategoryId { get; set; }
    }
}
