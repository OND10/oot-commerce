using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Response;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.CartsItems.Interface
{
    public interface ICartItemsService
    {
        Task<CartItemResponseDto> GetCartItemByIdAsync(int id);
        Task<IEnumerable<CartItemResponseDto>> GetCartItemByCartHeaderIdAsync(int cartHeaderId);
        Task<IEnumerable<CartItemResponseDto>> GetAllCartItemAsync();
        Task<CartItemResponseDto> AddCartItemAsync(CartItem entity);
        Task<CartItemResponseDto> UpdateCartItemAsync(CartItem entity);
        Task<bool> DeleteCartItemAsync(int id);
        Task<bool> DeleteCartItemByCartHeaderIdAsync(int cartHeaderId);
    }
}
