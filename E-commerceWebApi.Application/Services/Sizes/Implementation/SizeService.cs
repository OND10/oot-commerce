using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response;
using E_commerceWebApi.Application.Dtos.SizeDtos.Request;
using E_commerceWebApi.Application.Dtos.SizeDtos.Response;
using E_commerceWebApi.Application.Services.Sizes.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;

namespace E_commerceWebApi.Application.Services.Sizes.Implementation
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _respository;
        private readonly OnMapping _mapper;
        private readonly IUnitofWork _unitofWork;
        public SizeService(ISizeRepository repository, OnMapping mapper, IUnitofWork unitofWork)
        {
            _respository = repository;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        public async Task<Result<SizeResponseDto>> CreateAsync(SizeRequestDto entity)
        {
            var mappedModel = await _mapper.Map<SizeRequestDto, Size>(entity);

            var result = await _respository.CreateAsync(mappedModel.Data);

            await _unitofWork.SaveEntityChangesAsync();

            var mappedResponse = await _mapper.Map<Size, SizeResponseDto>(result);

            return await Result<SizeResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var result = await _respository.DeleteAsync(id);

            return await Result<bool>.SuccessAsync(result, ResponseStatus.DeletedSuccess, true);
        }

        public async Task<Result<IEnumerable<SizeResponseDto>>> GetAllAsync()
        {
            var result = await _respository.GetAllAsync();

            var mappedResponse = await _mapper.MapCollection<Size, SizeResponseDto>(result);

            return await Result< IEnumerable<SizeResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.DeletedSuccess, true);
        }
    }
}
