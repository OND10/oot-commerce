using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.CouponDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using E_commerceWebApi.Application.Services.Image.Interface;
using E_commerceWebApi.Application.Services.Products.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnMapper;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly OnMapping _mapper;
        private readonly IImageService _imageService;
        public ProductController(IProductService service, OnMapping mapper, IImageService imageService)
        {
            _mapper = mapper;
            _service = service;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<ProductResponseDto>>> Get(CancellationToken cancellationToken)
        {
            
            var result = await _service.GetAllAsync(cancellationToken);
            return await Result<IEnumerable<ProductResponseDto>>.SuccessAsync(result.Data, ResponseStatus.GetAllSuccess, true);
        }

        [HttpGet("{id}")]
        public async Task<Result<ProductDetailsDto>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);

            return await Result<ProductDetailsDto>.SuccessAsync(result.Data, ResponseStatus.GetSuccess, true);
        }

        [Authorize]
        [HttpPost]
        public async Task<Result<ProductResponseDto>> Post([FromForm] ProductRequestDto model, CancellationToken cancellationToken)
        {

            string imageUrl = (await _imageService.UploadImage(model, model.file)).ToString();

            model.ImageUrl = imageUrl;
            var result = await _service.CreateAsync(model, cancellationToken);

            if (result.IsSuccess)
            {
                return await Result<ProductResponseDto>.SuccessAsync(result.Data, ResponseStatus.CreateSuccess, true);
            }
            return await Result<ProductResponseDto>.FaildAsync(true, ResponseStatus.Faild);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<Result<ProductResponseDto>> Put([FromRoute] int id, [FromBody] ProductRequestDto model, CancellationToken cancellationToken)
        {
            try
            {
                var mappedRequestModel = await _mapper.Map<ProductRequestDto, UpdateProductRequestDto>(model);
                
                mappedRequestModel.Data.ProductId = id;
                
                var updateResult = await _service.UpdateAsync(mappedRequestModel.Data, cancellationToken);
                
                return await Result<ProductResponseDto>.SuccessAsync(updateResult.Data, ResponseStatus.UpdateSuccess, true);
            }
            catch (Exception)
            {
                return await Result<ProductResponseDto>.FaildAsync(true, ResponseStatus.Faild);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var delete = await _service.DeleteAsync(id, cancellationToken);
                if (delete.IsSuccess)
                {
                    return await Result<bool>.SuccessAsync(delete.Data, ResponseStatus.DeletedSuccess, true);
                }
                return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
            }
            catch (Exception)
            {
                throw new ArgumentNullException(nameof(id));
            }
        }

    }
}
