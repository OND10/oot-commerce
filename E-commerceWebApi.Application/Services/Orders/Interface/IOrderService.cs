using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.OrderDtos.Response;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request;

namespace E_commerceWebApi.Application.Services.Orders.Interface
{
    public interface IOrderService
    {
        Task<Result<OrderResponseDto>> CreateOrderAsync(CartDto request, CancellationToken cancellationToken);
        Task<Result<bool>> UpdateOrderStatusAsync(int orderId, string newStatus, CancellationToken cancellationToken);
        Task<Result<OrderResponseDto>> GetOrderById(int orderId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<OrderResponseDto>>>GetAllOrder(string userId, string role, CancellationToken cancellationToken);


    }
}
