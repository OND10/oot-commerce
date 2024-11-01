using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.CouponDtos.Request;
using E_commerceWebApi.Application.Dtos.CouponDtos.Response;

namespace E_commerceWebApi.Application.Services.Coupons.Interface
{
    public interface ICouponService
    {
       Task<Result<CouponResponseDto>> CreaAsync(CouponRequestDto model);
        Task<Result<IEnumerable<CouponResponseDto>>> GetAllAsync();
        Task<Result<CouponResponseDto>> GetByIdAsync(int id);
        Task<Result<CouponResponseDto>> GetByCodeAsync(string code);
        Task<Result<CouponResponseDto>> UpdateAsync(UpdateCouponRequestDto model);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
