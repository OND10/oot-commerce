using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response;
using E_commerceWebApi.Application.Services.Products.Interface;
using E_commerceWebApi.Application.Services.ProductsSizes.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.ProductsSizes.Implementation
{
    public class ProductSizesService : IProductSizesService
    {
        private readonly IProductSizesRepository _repository;
        private readonly OnMapping _mapper;
        private readonly IUnitofWork _unitofWork;
        public ProductSizesService(IProductSizesRepository repository, OnMapping mapper, IUnitofWork unitofWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        public async Task<Result<ProductSizesResponseDto>> CreateAsync(ProductSizesRequestDto entity)
        {

            
            var result = new ProductSizes();

            foreach (var size in entity.SizesId)
            {
                var mappedModel = await _mapper.Map<ProductSizesRequestDto, ProductSizes>(entity);
                mappedModel.Data.SizeId = size;
                result = await _repository.CreateAsync(mappedModel.Data);
            
            }
            await _unitofWork.SaveEntityChangesAsync();

            var mappedResponse = await _mapper.Map<ProductSizes, ProductSizesResponseDto>(result);

            return await Result<ProductSizesResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int Id)
        {
            var result = await _repository.DeleteAsync(Id);

            return await Result<bool>.SuccessAsync(result, ResponseStatus.DeletedSuccess, true);
        }

        public async Task<Result<IEnumerable<ProductSizesResponseDto>>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByProductIdAsync(productId, cancellationToken);

            var mappedResponse = await _mapper.MapCollection<ProductSizes, ProductSizesResponseDto>(result);

            return await Result<IEnumerable<ProductSizesResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<ProductSizesResponseDto>> UpdateAsync(ProductSizesRequestDto entity, CancellationToken cancellationToken)
        {
            // Fetch the existing product sizes for the given product
            var existingProductSizes = await _repository.GetByProductIdAsync(entity.ProductId, cancellationToken);

            // Identify sizes to remove (those in the database but not in the request)
            var sizesToRemove = existingProductSizes
                .Where(ps => !entity.SizesId.Contains(ps.SizeId))
                .ToList();

            // Identify sizes to add (those in the request but not in the database)
            var sizesToAdd = entity.SizesId
                .Where(sizeId => !existingProductSizes.Any(ps => ps.SizeId == sizeId))
                .ToList();

            // Remove the sizes that are no longer needed
            foreach (var sizeToRemove in sizesToRemove)
            {
                await _repository.DeleteAsync(sizeToRemove.Id);
            }

            var newProductSize = new ProductSizes();
            // Add new sizes
            foreach (var sizeId in sizesToAdd)
            {
                newProductSize = new ProductSizes
                {
                    ProductId = entity.ProductId,
                    SizeId = sizeId
                };

                await _repository.CreateAsync(newProductSize);
            }

            // Save the changes
            await _unitofWork.SaveEntityChangesAsync();

            // Map the response and return success
            var mappedResponse = await _mapper.Map<ProductSizes, ProductSizesResponseDto>(newProductSize);
            return await Result<ProductSizesResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.UpdateSuccess, true);

        }
    }
}
