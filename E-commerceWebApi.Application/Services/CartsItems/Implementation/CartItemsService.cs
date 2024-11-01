using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Response;
using E_commerceWebApi.Application.Services.CartsItems.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;

namespace E_commerceWebApi.Application.Services.CartsItems.Implementation
{
    public class CartItemsService : ICartItemsService
    {
        private readonly ICartItemRepository _repository;
        public CartItemsService(ICartItemRepository repository)
        {
            _repository = repository;
        }
        public Task<CartItemResponseDto> AddCartItemAsync(CartItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCartItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCartItemByCartHeaderIdAsync(int cartHeaderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItemResponseDto>> GetAllCartItemAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItemResponseDto>> GetCartItemByCartHeaderIdAsync(int cartHeaderId)
        {
            throw new NotImplementedException();
        }

        public Task<CartItemResponseDto> GetCartItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CartItemResponseDto> UpdateCartItemAsync(CartItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
