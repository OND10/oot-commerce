using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;
using E_commerceWebApi.Application.Services.ProductsColors.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;

namespace E_commerceWebApi.Application.Services.ProductsColors.Implementation
{
    public class ProductColorsService : IProductColorsService
    {
        private readonly IProductColorsRepository _repository;
        private readonly OnMapping _mapper;
        private readonly IUnitofWork _unitofWork;
        public ProductColorsService(IProductColorsRepository repository, OnMapping mapper, IUnitofWork unitofWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        public async Task<Result<ProductColorsResponseDto>> CreateAsync(ProductColorsRequestDto entity)
        {
            var result = new ProductColors();

            foreach (var color in entity.ColorsId)
            {
                var mappedModel = await _mapper.Map<ProductColorsRequestDto, ProductColors>(entity);

                mappedModel.Data.ColorId = color;

                result = await _repository.CreateAsync(mappedModel.Data);
            }

            await _unitofWork.SaveEntityChangesAsync();

            var mappedResponse = await _mapper.Map<ProductColors, ProductColorsResponseDto>(result);

            return await Result<ProductColorsResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int Id)
        {
            var result = await _repository.DeleteAsync(Id);


            return await Result<bool>.SuccessAsync(result, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<IEnumerable<ProductColorsResponseDto>>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByProductIdAsync(productId, cancellationToken);

            var mappedResponse = await _mapper.MapCollection<ProductColors, ProductColorsResponseDto>(result);

            return await Result<IEnumerable<ProductColorsResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<ProductColorsResponseDto>> UpdateAsync(ProductColorsRequestDto entity, CancellationToken cancellationToken)
        {
            // Fetch the existing product sizes for the given product
            var existingProductColors = await _repository.GetByProductIdAsync(entity.ProductId, cancellationToken);

            // Identify sizes to remove (those in the database but not in the request)
            var colorsToRemove = existingProductColors
                .Where(ps => !entity.ColorsId.Contains(ps.ColorId))
                .ToList();

            // Identify sizes to add (those in the request but not in the database)
            var colorsToAdd = entity.ColorsId
                .Where(sizeId => !existingProductColors.Any(ps => ps.ColorId == sizeId))
                .ToList();

            // Remove the sizes that are no longer needed
            foreach (var colorToRemove in colorsToRemove)
            {
                await _repository.DeleteAsync(colorToRemove.Id);
            }

            var newProductColor = new ProductColors();
            // Add new sizes
            foreach (var colorId in colorsToAdd)
            {
                newProductColor = new ProductColors
                {
                    ProductId = entity.ProductId,
                    ColorId = colorId
                };

                await _repository.CreateAsync(newProductColor);
            }

            // Save the changes
            await _unitofWork.SaveEntityChangesAsync();

            // Map the response and return success
            var mappedResponse = await _mapper.Map<ProductColors, ProductColorsResponseDto>(newProductColor);
            return await Result<ProductColorsResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.UpdateSuccess, true);
        }
    }
}
