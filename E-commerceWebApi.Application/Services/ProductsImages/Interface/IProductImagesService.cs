using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductImagesDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductImagesDtos.Response;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace E_commerceWebApi.Application.Services.ProductsImages.Interface
{
    public interface IProductImagesService
    {
        Task<Result<ProductImagesResponseDto>> UploadAsync(IEnumerable<IFormFile> files, int productId, string rootPath);
        Task<Result<IEnumerable<ProductImagesResponseDto>>> FindAllAsync(Expression<Func<ProductImagesRequestDto, bool>> expression, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ProductImagesResponseDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<IEnumerable<ProductImagesResponseDto>>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(int imageId, CancellationToken cancellationToken);
        Task<Result<ProductImagesResponseDto>> UpdateAsync(int imageId, IFormFile file, CancellationToken cancellationToken);
    }
}
