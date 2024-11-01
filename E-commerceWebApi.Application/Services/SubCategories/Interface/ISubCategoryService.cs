using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.SubCategoryDtos.Request;
using E_commerceWebApi.Application.Dtos.SubCategoryDtos.Response;

namespace E_commerceWebApi.Application.Services.SubCategories.Interface
{
    public interface ISubCategoryService
    {
        Task<Result<SubCategoryResponseDto>> CreateAsync(SubCategoryRequestDto entity);
        Task<Result<IEnumerable<SubCategoryResponseDto>>> GetAllAsync();
        Task<Result<SubCategoryResponseDto>> GetByIdAsync(int id);
        Task<Result<SubCategoryResponseDto>> GetByCategoryIdAsync(int categoryId);
        Task<Result<SubCategoryResponseDto>> UpdateAsync(UpdateSubCategoryRequestDto entity);
        Task<Result<bool>> DeleteAsync(int Id);
    }
}
