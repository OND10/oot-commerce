using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request;
using E_commerceWebApi.Application.Services.Carts.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        public CartController(ICartService service) 
        {
            _service = service;
        }


        [HttpGet("{userId}")]
        public async Task<Result<IEnumerable<CartDto>>> Get(string userId, CancellationToken cancellationToken)
        {

            var result = await _service.GetAllCartsAsync(userId, cancellationToken);
            if (result != null && result.IsSuccess)
            {
                return await Result<IEnumerable<CartDto>>.SuccessAsync(result.Data, "Viewed Successfully", true);
            }

            return await Result<IEnumerable<CartDto>>.FaildAsync(false, "Not Viewed Successfully");
        }

        [HttpPost("ApplyCoupon")]
        public async Task<Result<bool>> ApplyCoupon([FromBody] CartDto cart, CancellationToken cancellationToken)
        {
            

            var result = await _service.ApplyCartCoupon(cart, cancellationToken);
            if (result.IsSuccess)
            {

                return await Result<bool>.SuccessAsync(true, "Applied Successfully", true);
            }

            return await Result<bool>.FaildAsync(false, "Applied Successfully");
        }

        [HttpPost("RemoveCoupon")]
        public async Task<Result<bool>> RemoveCoupon([FromBody] CartDto cart, CancellationToken cancellationToken)
        {

            var result = await _service.RemoveCartCoupon(cart, cancellationToken);
            if (result.IsSuccess)
            {

                return await Result<bool>.SuccessAsync(true, "Applied Successfully", true);
            }

            return await Result<bool>.FaildAsync(false, "Applied Successfully");
        }


        [HttpPost("createCart")]
        public async Task<Result<CartDto>> Post(CartDto cartDto, CancellationToken cancellationToken)
        {

            var result = await _service.AddCartAsync(cartDto, cancellationToken);

            if (result.IsSuccess)
            {
                return await Result<CartDto>.SuccessAsync(result.Data, "Added Successfuly", true);
            }

            return await Result<CartDto>.FaildAsync(false, "Not added");
        }

        [HttpPost("removeCart")]
        public async Task<Result<bool>> Delete([FromBody] int cartItemsId, CancellationToken cancellationToken)
        {
            

            var result = await _service.DeleteCartAsync(cartItemsId, cancellationToken);

            if (result.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(true, "Deleted Successfuly", true);
            }

            return await Result<bool>.FaildAsync(false, "Not Deleted");
        }

    }
}
