using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Response;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.Carts.Interface
{
    public interface ICartService
    {
        Task<Result<CartResponseDto>> GetCartByIdAsync(Guid id);
        Task<Result<CartResponseDto>> GetCartByUserIdAsync(string userId);
        Task<Result<IEnumerable<CartDto>>> GetAllCartsAsync(string userId, CancellationToken cancellationToken);
        Task<Result<CartDto>> AddCartAsync(CartDto request, CancellationToken cancellationToken);
        Task<Result<CartResponseDto>> UpdateCartAsync(CartDto request);
        Task<Result<bool>> DeleteCartAsync(int cartItemid, CancellationToken cancellationToken);

        Task<Result<bool>> ApplyCartCoupon(CartDto request, CancellationToken cancellationToken);
        Task<Result<bool>> RemoveCartCoupon(CartDto request, CancellationToken cancellationToken);

    }
}
