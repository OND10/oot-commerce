using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.CategoryDtos.Request;
using E_commerceWebApi.Application.Dtos.CategoryDtos.Response;
using E_commerceWebApi.Application.Services.Categories.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.Categories.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly OnMapping _mapper;
        public CategoryService(ICategoryRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CategoryResponseDto>> CreateAsync(CategoryRequestDto entity, CancellationToken cancellationToken)
        {
            var mappedModel = await _mapper.Map<CategoryRequestDto, Category>(entity);

            var result = await _repository.CreateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Category, CategoryResponseDto>(result);

            return await Result<CategoryResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int Id, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteAsync(Id);

            return await Result<bool>.SuccessAsync(result, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<IEnumerable<CategoryResponseDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();

            var mappedResponse = await _mapper.MapCollection<Category, CategoryResponseDto>(result);

            return await Result<IEnumerable<CategoryResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<CategoryResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(id);

            var mappedResponse = await _mapper.Map<Category, CategoryResponseDto>(result);

            return await Result<CategoryResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequestDto entity, CancellationToken cancellationToken)
        {
            var mappedModel = await _mapper.Map<UpdateCategoryRequestDto, Category>(entity);

            var result = await _repository.UpdateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Category, CategoryResponseDto>(result);

            return await Result<CategoryResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.UpdateSuccess, true);
        }

    }
}
