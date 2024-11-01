using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.SizeDtos.Request;
using E_commerceWebApi.Application.Dtos.SizeDtos.Response;

namespace E_commerceWebApi.Application.Services.Sizes.Interface
{
    public interface ISizeService
    {
        Task<Result<SizeResponseDto>> CreateAsync(SizeRequestDto entity);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<IEnumerable<SizeResponseDto>>> GetAllAsync();

    }
}
