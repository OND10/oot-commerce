using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response;
using E_commerceWebApi.Application.Services.ProductsSizes.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : ControllerBase
    {
        private readonly IProductSizesService _service;
        public ProductSizesController(IProductSizesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<ProductSizesResponseDto>>> Get(int productId, CancellationToken cancellationToken)
        {
            var response = await _service.GetByProductIdAsync(productId, cancellationToken);

            if (response.IsSuccess)
            {
                return await Result<IEnumerable<ProductSizesResponseDto>>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<IEnumerable<ProductSizesResponseDto>>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPost]
        public async Task<Result<ProductSizesResponseDto>> Post(ProductSizesRequestDto request)
        {
            var response = await _service.CreateAsync(request);

            if (response.IsSuccess)
            {
                return await Result<ProductSizesResponseDto>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<ProductSizesResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPut("updateProductSize/{productId}")]
        public async Task<Result<ProductSizesResponseDto>> Put(int productId, ProductSizesRequestDto request, CancellationToken cancellationToken)
        {
            request.ProductId = productId;
            
            var response = await _service.UpdateAsync(request, cancellationToken);
            
            if (response.IsSuccess)
            {
                return await Result<ProductSizesResponseDto>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<ProductSizesResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpDelete]
        public async Task<Result<bool>> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);

        }
    }

}
