using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response;
using E_commerceWebApi.Application.Services.ProductsColors.Interface;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorsController : ControllerBase
    {
        private readonly IProductColorsService _service;
        public ProductColorsController(IProductColorsService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<Result<IEnumerable<ProductColorsResponseDto>>>Get(int productId, CancellationToken cancellationToken)
        {
            var response = await _service.GetByProductIdAsync(productId, cancellationToken);

            if(response.IsSuccess)
            {
                return await Result<IEnumerable<ProductColorsResponseDto>>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<IEnumerable<ProductColorsResponseDto>>.FaildAsync(false, ResponseStatus.Faild);

        }

        [HttpPost]
        public async Task<Result<ProductColorsResponseDto>>Post(ProductColorsRequestDto request)
        {
            var response = await _service.CreateAsync(request);

            if (response.IsSuccess)
            {
                return await Result<ProductColorsResponseDto>.SuccessAsync(response.Data, ResponseStatus.CreateSuccess, true);
            }

            return await Result<ProductColorsResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPut("updateProductColor/{productId}")]
        public async Task<Result<ProductColorsResponseDto>> Put(int productId, ProductColorsRequestDto request, CancellationToken cancellationToken)
        {
            request.ProductId = productId;

            var response = await _service.UpdateAsync(request, cancellationToken);

            if (response.IsSuccess)
            {
                return await Result<ProductColorsResponseDto>.SuccessAsync(response.Data, ResponseStatus.UpdateSuccess, true);
            }

            return await Result<ProductColorsResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpDelete]
        public async Task<Result<bool>>Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, ResponseStatus.DeletedSuccess, true);
            }

            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);

        }
    }
}
