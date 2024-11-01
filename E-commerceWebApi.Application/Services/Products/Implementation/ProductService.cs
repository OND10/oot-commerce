using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductImagesDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response;
using E_commerceWebApi.Application.Services.Products.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;

namespace E_commerceWebApi.Application.Services.Products.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductColorsRepository _colorsRepository;
        private readonly IProductSizesRepository _sizesRepository;
        private readonly IProductImagesRepository _imagesRepository;
        private readonly OnMapping _mapper;
        public ProductService(IProductRepository repository, 
            IProductImagesRepository imagesRepository,
            IProductColorsRepository colorsRepository,
            IProductSizesRepository sizesRepository,
            OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _colorsRepository = colorsRepository;
            _sizesRepository = sizesRepository;
            _imagesRepository = imagesRepository;
        }

        public async Task<Result<ProductResponseDto>> CreateAsync(ProductRequestDto entity, CancellationToken cancellationToken)
        {
            var mappedModel = await _mapper.Map<ProductRequestDto, Product>(entity);

            mappedModel.Data.CreatedAt = DateTime.UtcNow;
            //mappedModel.Data.DeletedBy = "none";
            //mappedModel.Data.ModifiedBy = "none";
            var result = await _repository.CreateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Product, ProductResponseDto>(result);

            return await Result<ProductResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int Id, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteAsync(Id);

            return await Result<bool>.SuccessAsync(result, ResponseStatus.DeletedSuccess, true);
        }

        public async Task<Result<IEnumerable<ProductResponseDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();

            var mappedResponse = await _mapper.MapCollection<Product, ProductResponseDto>(result);

            return await Result<IEnumerable<ProductResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetAllSuccess, true);
        }

        public async Task<Result<ProductDetailsDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id);

            var mappedProducts = await _mapper.Map<Product, ProductResponseDto>(product);

            var images = await _imagesRepository.GetByProductIdAsync(id, cancellationToken);

            var mappedImages = await _mapper.MapCollection<ProductImages, ProductImagesResponseDto>(images);

            var sizes = await _sizesRepository.GetByProductIdAsync(id, cancellationToken);

            var mappedSizes = await _mapper.MapCollection<ProductSizes, ProductSizesResponseDto>(sizes);

            var colors = await _colorsRepository.GetByProductIdAsync(id, cancellationToken);

            var mappedColors = await _mapper.MapCollection<ProductColors, ProductColorsResponseDto>(colors);


            var result = new ProductDetailsDto
            {
                Images = mappedImages.Data,
                Sizes = mappedSizes.Data,
                Colors = mappedColors.Data,
                ArabicName = mappedProducts.Data.ArabicName,
                EnglishDescription = mappedProducts.Data.EnglishDescription,
                EnglishName = mappedProducts.Data.EnglishName,
                ArabicDescription = mappedProducts.Data.ArabicDescription,
                Quantity = mappedProducts.Data.Quantity,
                Price = mappedProducts.Data.Price,
                DiscountPrice = mappedProducts.Data.DiscountPrice,
                MainImageUrl = mappedProducts.Data.ImageUrl,
                IsFeatured = mappedProducts.Data.IsFeatured,
                SubcategoryId = mappedProducts.Data.SubcategoryId,
                ProductId = id
            };


            return await Result<ProductDetailsDto>.SuccessAsync(result, ResponseStatus.GetSuccess, true);
        }

        public Task<Pagination<Result<ProductResponseDto>>> GetPaginatedProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ProductResponseDto>> UpdateAsync(UpdateProductRequestDto entity, CancellationToken cancellationToken)
        {
            var mappedModel = await _mapper.Map<UpdateProductRequestDto, Product>(entity);

            mappedModel.Data.Id = entity.ProductId;

            var result = await _repository.UpdateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Product, ProductResponseDto>(result);

            return await Result<ProductResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.UpdateSuccess, true);
        }
    }
}
