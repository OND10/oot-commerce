using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.RateDtos.Request;
using E_commerceWebApi.Application.Dtos.RateDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.Rate
{
    public interface IRateService
    {
        Task<Result<RateResponseDto>> AddRate(RateRequestDto request);
        Task<Result<ProductRateResponseDto>> GetProductsRate(int product);
        Task<Result<double>> GetavarageForProduct(int product);
    }
}
