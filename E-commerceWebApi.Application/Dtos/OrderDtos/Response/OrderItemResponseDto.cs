using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.OrderDtos.Response
{
    public class OrderItemResponseDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductResponseDto Product { get; set; }
        public int Count { get; set; }
        public string EnglishProductName { get; set; }
        public string ArabicProductName { get; set; }
        public float Price { get; set; }
    }
}
