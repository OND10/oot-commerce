using E_commerceWebApi.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IProductImagesRepository
    {
        Task<ProductImages> UploadAsync(IEnumerable<IFormFile> files, int productId, string rootPath);
        Task<IEnumerable<ProductImages>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ProductImages>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int imageId, CancellationToken cancellationToken);
        Task<ProductImages> UpdateAsync(int imageId, IFormFile file);
    }
}
