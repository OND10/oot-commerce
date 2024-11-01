using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductImagesDtos.Response;
using E_commerceWebApi.Application.Services.ProductsImages.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImagesService _service;
        public ProductImagesController(IProductImagesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<ProductImagesResponseDto>>> Get(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(cancellationToken);

            return await Result<IEnumerable<ProductImagesResponseDto>>.SuccessAsync(result.Data, ResponseStatus.GetAllSuccess, true);
        }

        [HttpGet("getImage/{productId}")]
        public async Task<Result<IEnumerable<ProductImagesResponseDto>>> Get(int productId, CancellationToken cancellationToken)
        {
            var result = await _service.GetByProductIdAsync(productId, cancellationToken);

            if (result.IsSuccess)
            {
                return await Result<IEnumerable<ProductImagesResponseDto>>.SuccessAsync(result.Data, ResponseStatus.GetSuccess, true);

            }

            return await Result<IEnumerable<ProductImagesResponseDto>>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPost]
        public async Task<Result<IEnumerable<ProductImagesResponseDto>>> UploadImages([FromForm] IEnumerable<IFormFile> files, [FromForm] int productId, CancellationToken cancellationToken)
        {
            var result = await _service.UploadAsync(files, productId, "");

            if (result.IsSuccess)
            {
                return await Result<IEnumerable<ProductImagesResponseDto>>.SuccessAsync(ResponseStatus.UploadedSuccess, true);
            }


            return await Result<IEnumerable<ProductImagesResponseDto>>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpDelete("{imageId}")]
        public async Task<Result<bool>> DeleteImage(int imageId, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteAsync(imageId, cancellationToken);
            if (result.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(ResponseStatus.DeletedSuccess, true);
            }

            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPut("{imageId}")]
        public async Task<Result<ProductImagesResponseDto>> UpdateImage(int imageId, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(imageId, file, cancellationToken);
            if (result.IsSuccess)
            {
                return await Result<ProductImagesResponseDto>.SuccessAsync(result.Data, ResponseStatus.UpdateSuccess, true);
            }

            return await Result<ProductImagesResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

    }
}
