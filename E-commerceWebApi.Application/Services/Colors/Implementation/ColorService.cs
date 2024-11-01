using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ColorDtos.Request;
using E_commerceWebApi.Application.Dtos.ColorDtos.Response;
using E_commerceWebApi.Application.Services.Colors.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;

namespace E_commerceWebApi.Application.Services.Colors.Implementation
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _respository;
        private readonly OnMapping _mapper;
        private readonly IUnitofWork _unitofWork;
        public ColorService(IColorRepository repository, OnMapping mapper, IUnitofWork unitofWork)
        {
            _respository = repository;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }


        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var result = await _respository.DeleteAsync(id);

            return await Result<bool>.SuccessAsync(result, ResponseStatus.DeletedSuccess, true);
        }

        public async Task<Result<IEnumerable<ColorResponseDto>>> GetAllAsync()
        {
            var result = await _respository.GetAllAsync();

            var mappedResponse = await _mapper.MapCollection<Color, ColorResponseDto>(result);
            
            return await Result<IEnumerable<ColorResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetAllSuccess, true);
        }

        public async Task<Result<ColorResponseDto>> CreateAsync(ColorRequestDto entity)
        {
            var mappedModel = await _mapper.Map<ColorRequestDto, Color>(entity);

            var result = await _respository.CreateAsync(mappedModel.Data);

            await _unitofWork.SaveEntityChangesAsync();

            var mappedResponse = await _mapper.Map<Color, ColorResponseDto>(result);

            return await Result<ColorResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }
    }
}
