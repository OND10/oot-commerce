using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using E_commerceWebApi.Domain.Entities;

namespace E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Response
{
    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductResponseDto? Product { get; set; }
        public int Count { get; set; }
    }
}
