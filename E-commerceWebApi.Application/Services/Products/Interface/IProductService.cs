using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using E_commerceWebApi.Domain.Shared;

namespace E_commerceWebApi.Application.Services.Products.Interface
{
    public interface IProductService
    {
        Task<Result<ProductResponseDto>> CreateAsync(ProductRequestDto entity, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ProductResponseDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<ProductDetailsDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Pagination<Result<ProductResponseDto>>> GetPaginatedProduct();
        Task<Result<ProductResponseDto>> UpdateAsync(UpdateProductRequestDto entity, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(int Id, CancellationToken cancellationToken);
    }
}
