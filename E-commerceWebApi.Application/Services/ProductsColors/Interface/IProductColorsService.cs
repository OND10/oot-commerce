using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;

namespace E_commerceWebApi.Application.Services.ProductsColors.Interface
{
    public interface IProductColorsService
    {
        Task<Result<ProductColorsResponseDto>>CreateAsync(ProductColorsRequestDto entity);
        Task<Result<ProductColorsResponseDto>> UpdateAsync(ProductColorsRequestDto entity, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(int Id);
        Task<Result<IEnumerable<ProductColorsResponseDto>>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
    }
}
