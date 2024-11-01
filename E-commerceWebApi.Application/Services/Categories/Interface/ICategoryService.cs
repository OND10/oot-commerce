using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.CategoryDtos.Request;
using E_commerceWebApi.Application.Dtos.CategoryDtos.Response;

namespace E_commerceWebApi.Application.Services.Categories.Interface
{
    public interface ICategoryService
    {
        Task<Result<CategoryResponseDto>> CreateAsync(CategoryRequestDto entity, CancellationToken cancellationToken);
        Task<Result<IEnumerable<CategoryResponseDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<CategoryResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequestDto entity, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(int Id, CancellationToken cancellationToken);
    }
}
