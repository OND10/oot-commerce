using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.CouponDtos.Request;
using E_commerceWebApi.Application.Dtos.CouponDtos.Response;
using E_commerceWebApi.Application.Services.Coupons.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;

namespace E_commerceWebApi.Application.Services.Coupons.Implementation
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _repository;
        private readonly OnMapping _mapper;
        public CouponService(ICouponRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CouponResponseDto>> CreaAsync(CouponRequestDto model)
        {
            var mappedModel = await _mapper.Map<CouponRequestDto, Coupon>(model);

            var result = await _repository.CreateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Coupon, CouponResponseDto>(result);

            return await Result<CouponResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);

            return await Result<bool>.SuccessAsync(result, ResponseStatus.DeletedSuccess, true);
        }

        public async Task<Result<IEnumerable<CouponResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            var mappedResponse = await _mapper.MapCollection<Coupon, CouponResponseDto>(result);

            return await Result<IEnumerable<CouponResponseDto>>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetAllSuccess, true);
        }

        public async Task<Result<CouponResponseDto>> GetByCodeAsync(string code)
        {
            var result = await _repository.GetByCodeAsync(code);

            var mappedResponse = await _mapper.Map<Coupon, CouponResponseDto>(result);

            return await Result<CouponResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<CouponResponseDto>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            var mappedResponse = await _mapper.Map<Coupon, CouponResponseDto>(result);

            return await Result<CouponResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<CouponResponseDto>> UpdateAsync(UpdateCouponRequestDto model)
        {
            var mappedModel = await _mapper.Map<UpdateCouponRequestDto, Coupon>(model);

            var result = await _repository.UpdateAsync(mappedModel.Data);

            var mappedResponse = await _mapper.Map<Coupon, CouponResponseDto>(result);

            return await Result<CouponResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.UpdateSuccess, true);
        }
    }
}
