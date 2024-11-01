using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.OrderDtos.Response;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request;
using E_commerceWebApi.Application.Services.Orders.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service )
        {
            _service = service;
        }


        [Authorize]
        [HttpGet("GetOrders")]
        public async Task<Result<IEnumerable<OrderResponseDto>>> Get(string? userId = "", CancellationToken cancellationToken = default)
        {
            if (User.IsInRole("Admin"))
            {


                var response = await _service.GetAllOrder(null, "Admin", cancellationToken);

                if (response.IsSuccess)
                {
                    return await Result<IEnumerable<OrderResponseDto>>.SuccessAsync(response.Data, "GetAll order Successfully", true);
                }
                return await Result<IEnumerable<OrderResponseDto>>.FaildAsync(false, "Operation faild");

            }
            else
            {

                var response = await _service.GetAllOrder(userId, "User", cancellationToken);

                if (response.IsSuccess)
                {
                    return await Result<IEnumerable<OrderResponseDto>>.SuccessAsync(response.Data, "GetAll user order Successfully", true);
                }
                return await Result<IEnumerable<OrderResponseDto>>.FaildAsync(false, "Operation faild");

            }
        }

        [HttpGet("GetOrder/{id:int}")]
        public async Task<Result<OrderResponseDto>> Get(int id, CancellationToken cancellationToken)
        {
            

            var response = await _service.GetOrderById(id, cancellationToken);

            if (response.IsSuccess)
            {

                return await Result<OrderResponseDto>.SuccessAsync(response.Data, ResponseStatus.GetSuccess, true);
            }

            return await Result<OrderResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPost("CreateOrder")]
        public async Task<Result<OrderResponseDto>> Post(CartDto cartDto, CancellationToken cancellationToken)
        {

            var response = await _service.CreateOrderAsync(cartDto, cancellationToken);
            if (response.IsSuccess)
            {
                return await Result<OrderResponseDto>.SuccessAsync(response.Data, ResponseStatus.CreateSuccess, true);
            }

            return await Result<OrderResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPost("UpdateOrderStatus/{orderId:int}")]
        public async Task<Result<bool>> UpdateOrderStatus(int orderId, [FromBody] string newStatus, CancellationToken cancellationToken)
        {
            

            var response = await _service.UpdateOrderStatusAsync(orderId, newStatus, cancellationToken);

            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(true, "OrderStatus is updated Successfully", true);
            }


            return await Result<bool>.FaildAsync(false, "OrderStatus is not updated");
        }
    }
}
