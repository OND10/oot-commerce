using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.RateDtos.Request;
using E_commerceWebApi.Application.Dtos.RateDtos.Response;
using E_commerceWebApi.Application.Services.Rate;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {

        private readonly IRateService _service;

        public RateController(IRateService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<Result<ProductRateResponseDto>> GetProductsRates(int ProductId)
        {
            var rate = await _service.GetProductsRate(ProductId);
            if (rate.IsSuccess)
            {
                return await Result<ProductRateResponseDto>.SuccessAsync(rate.Data, rate.Message, true);
            }

            return await Result<ProductRateResponseDto>.FaildAsync(false, rate.Message);
        }

        [HttpGet]

        public async Task<Result<double>> GetAvarageforProduct(int ProductId)
        {
            var rate = await _service.GetavarageForProduct(ProductId);
            if (rate.IsSuccess)
            {
                return await Result<double>.SuccessAsync(rate.Data, rate.Message, true);
            }

            return await Result<double>.FaildAsync(false, rate.Message);

        }

        [HttpPost]

        public async Task<Result<RateResponseDto>> AddUserRate(RateRequestDto request)
        {
            var rate = await _service.AddRate(request);
            if (rate.IsSuccess)
            {
                return await Result<RateResponseDto>.SuccessAsync(rate.Data, rate.Message, true);
            }
            return await Result<RateResponseDto>.FaildAsync(false, rate.Message);
        }

    }
}
