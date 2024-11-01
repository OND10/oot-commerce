using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.BrandDtos.Request;
using E_commerceWebApi.Application.Dtos.BrandDtos.Response;
using E_commerceWebApi.Application.Dtos.BrandDtos.Request;
using E_commerceWebApi.Application.Dtos.BrandDtos.Response;
using E_commerceWebApi.Application.Services.Brands.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.Brands.Implementation
{
    public class BrandService : IBrandService
    {

        private readonly IBrandRepository _repository;
        private readonly OnMapping _mapper;
        public BrandService(IBrandRepository repository, OnMapping mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<BrandResponseDto>> CreateAsync(BrandRequestDto entity, CancellationToken cancellationToken)
        {
            var mappedModel = await _mapper.Map<BrandRequestDto, Brand>(entity);

            var result = await _repository.CreateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Brand, BrandResponseDto>(result);

            return await Result<BrandResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int Id, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteAsync(Id);

            return await Result<bool>.SuccessAsync(result, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<IEnumerable<BrandResponseDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();

            var mappedResponse = await _mapper.MapCollection<Brand, BrandResponseDto>(result);

            return await Result<IEnumerable<BrandResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<BrandResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(id);

            var mappedResponse = await _mapper.Map<Brand, BrandResponseDto>(result);

            return await Result<BrandResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<BrandResponseDto>> UpdateAsync(UpdateBrandRequestDto entity, CancellationToken cancellationToken)
        {
            var mappedModel = await _mapper.Map<UpdateBrandRequestDto, Brand>(entity);

            var result = await _repository.UpdateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Brand, BrandResponseDto>(result);

            return await Result<BrandResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.UpdateSuccess, true);
        }

    }
}
