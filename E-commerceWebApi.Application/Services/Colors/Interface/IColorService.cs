using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ColorDtos.Request;
using E_commerceWebApi.Application.Dtos.ColorDtos.Response;

namespace E_commerceWebApi.Application.Services.Colors.Interface
{
    public interface IColorService
    {
        Task<Result<ColorResponseDto>> CreateAsync(ColorRequestDto entity);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<IEnumerable<ColorResponseDto>>> GetAllAsync();
    }
}
