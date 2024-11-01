using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.CouponDtos.Request;
using E_commerceWebApi.Application.Dtos.CouponDtos.Response;
using E_commerceWebApi.Application.Services.Coupons.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMapper;
using Stripe;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _service;
        public CouponController(ICouponService service)
        {
            _service = service;
        }

        // GET: api/<CouponController>
        [HttpGet]
        public async Task<Result<IEnumerable<CouponResponseDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            return await Result<IEnumerable<CouponResponseDto>>.SuccessAsync(result.Data, "Viewed Successfully", true);
        }

        //GET api/<CouponController>/5
        //[HttpGet("{id}")]
        //[Authorize(Roles = "ADMIN")]
        //public async Task<Result<CouponResponseDto>> GetId(int id)
        //{
        //    var findId = await _service.GetByIdAsync(id);
        //    return await Result<CouponResponseDto>.SuccessAsync(findId.Data, "Found Successfully", true);
        //}


        [HttpGet("{code}")]
        public async Task<Result<CouponResponseDto>> GetCode(string code)
        {
            var findCode = await _service.GetByCodeAsync(code);
            return await Result<CouponResponseDto>.SuccessAsync(findCode.Data, "Found Successfullt", true);
        }

        //POST api/<CouponController>
        [HttpPost]
        public async Task<Result<CouponResponseDto>> Post([FromBody] CouponRequestDto model)
        {
            try
            {
                var add = await _service.CreaAsync(model);

                
                var request = new UpdateCouponRequestDto()
                {
                    CouponId = add.Data.Id,
                    CouponCode = add.Data.CouponCode,
                    DiscountAmount = add.Data.DiscountAmount,
                    MinAmount = add.Data.MinAmount,
                };

                await _service.UpdateAsync(request);

                return await Result<CouponResponseDto>.SuccessAsync(add.Data, "Created Successfully", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating coupon: {ex.Message}");
                throw new ArgumentNullException(nameof(model), ex);
            }


        }

        //PUT api/<CouponController>/5

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<Result<CouponResponseDto>> Put([FromRoute] int id, [FromBody] CouponRequestDto model)
        {
            try
            {
                var mapper = new OnMapping();
                var mappedRequestModel = await mapper.Map<CouponRequestDto, UpdateCouponRequestDto>(model);
                mappedRequestModel.Data.CouponId = id;
                var updateResult = await _service.UpdateAsync(mappedRequestModel.Data);
                return await Result<CouponResponseDto>.SuccessAsync(updateResult.Data, "Updated Successfully", true);
            }
            catch (Exception)
            {
                return await Result<CouponResponseDto>.FaildAsync(true, "Not Updated");
            }
        }

        // DELETE api/<CouponController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                
                var result = await _service.DeleteAsync(id);

                return await Result<bool>.SuccessAsync(result.Data, "Deleted Successfully", true);
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"General error: {ex.Message}");
                throw new ArgumentNullException(nameof(id), ex);
            }

        }
    }
}
