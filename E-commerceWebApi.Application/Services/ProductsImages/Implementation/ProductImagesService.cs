using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductImagesDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductImagesDtos.Response;
using E_commerceWebApi.Application.Services.Products.Interface;
using E_commerceWebApi.Application.Services.ProductsImages.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Http;
using OnMapper;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.ProductsImages.Implementation
{
    public class ProductImagesService : IProductImagesService
    {
        private readonly IProductImagesRepository _repository;
        private readonly OnMapping _mapper;
        public ProductImagesService(IProductImagesRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<Result<IEnumerable<ProductImagesResponseDto>>> FindAllAsync(Expression<Func<ProductImagesRequestDto, bool>> expression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<ProductImagesResponseDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);

            var mappedResponse = await _mapper.MapCollection<ProductImages, ProductImagesResponseDto>(result);

            return await Result<IEnumerable<ProductImagesResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetAllSuccess, true);
        }

        public async Task<Result<IEnumerable<ProductImagesResponseDto>>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {

            var result = await _repository.GetByProductIdAsync(productId, cancellationToken);

            var mappedResponse = await _mapper.MapCollection<ProductImages, ProductImagesResponseDto>(result);

            return await Result<IEnumerable<ProductImagesResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetAllSuccess, true);
        }

        public async Task<Result<ProductImagesResponseDto>> UploadAsync(IEnumerable<IFormFile> files, int productId, string rootPath)
        {

            foreach (var file in files)
            {
                var imgValidateResult = ImageValidation.ValidationFileUpload(file);

                if (imgValidateResult is false)
                {
                    return await Result<ProductImagesResponseDto>.FaildAsync(false, ResponseStatus.ImageNotValid);
                }
            }

            var result = await _repository.UploadAsync(files, productId, "");

            var mappedResult = new ProductImagesResponseDto
            {
                ImageExtension = result.ImageExtension,
                IsMainImage = result.IsMainImage,
                ImageName = result.ImageName,
                ImageUrl = result.ImageUrl,
                ProductId = result.ProductId,
                Id = result.Id,
            };

            return await Result<ProductImagesResponseDto>.SuccessAsync(mappedResult, ResponseStatus.UploadedSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int imageId, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteAsync(imageId, cancellationToken);
            if (result)
            {
                return await Result<bool>.SuccessAsync(true, ResponseStatus.DeletedSuccess, true);
            }
            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
        }

        public async Task<Result<ProductImagesResponseDto>> UpdateAsync(int imageId, IFormFile file, CancellationToken cancellationToken)
        {
            var updatedImage = await _repository.UpdateAsync(imageId, file);
            if (updatedImage != null)
            {
                var response = new ProductImagesResponseDto
                {
                    Id = updatedImage.Id,
                    ProductId = updatedImage.ProductId,
                    ImageName = updatedImage.ImageName,
                    ImageUrl = updatedImage.ImageUrl,
                    ImageExtension = updatedImage.ImageExtension,
                    IsMainImage = updatedImage.IsMainImage
                };
                return await Result<ProductImagesResponseDto>.SuccessAsync(response, ResponseStatus.UpdateSuccess, true);
            }
            return await Result<ProductImagesResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

    }

}
