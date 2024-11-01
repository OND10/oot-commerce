using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response;
using E_commerceWebApi.Application.Dtos.SubCategoryDtos.Request;
using E_commerceWebApi.Application.Dtos.SubCategoryDtos.Response;
using E_commerceWebApi.Application.Services.SubCategories.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;

namespace E_commerceWebApi.Application.Services.SubCategories.Implementation
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubcategoryRepository _repository;
        private readonly OnMapping _mapper;
        public SubCategoryService(ISubcategoryRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<SubCategoryResponseDto>> CreateAsync(SubCategoryRequestDto entity)
        {
            var mappedModel = await _mapper.Map<SubCategoryRequestDto, Subcategory>(entity);

            var result = await _repository.CreateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Subcategory, SubCategoryResponseDto>(result);

            return await Result<SubCategoryResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int Id)
        {
            var result = await _repository.DeleteAsync(Id);

            return await Result<bool>.SuccessAsync(result, ResponseStatus.DeletedSuccess, true);
        }

        public async Task<Result<IEnumerable<SubCategoryResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            var mappedResponse = await _mapper.MapCollection<Subcategory, SubCategoryResponseDto>(result);

            return await Result<IEnumerable<SubCategoryResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetAllSuccess, true);
        }

        public async Task<Result<SubCategoryResponseDto>> GetByCategoryIdAsync(int categoryId)
        {
            var result = await _repository.GetByCategoryIdAsync(categoryId);

            var mappedResponse = await _mapper.Map<Subcategory, SubCategoryResponseDto>(result);

            return await Result<SubCategoryResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<SubCategoryResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            var mappedResponse = await _mapper.Map<Subcategory, SubCategoryResponseDto>(result);

            return await Result<SubCategoryResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<SubCategoryResponseDto>> UpdateAsync(UpdateSubCategoryRequestDto entity)
        {
            var mappedModel = await _mapper.Map<UpdateSubCategoryRequestDto, Subcategory>(entity);

            var result = await _repository.CreateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Subcategory, SubCategoryResponseDto>(result);

            return await Result<SubCategoryResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.UpdateSuccess, true);
        }
    }
}
