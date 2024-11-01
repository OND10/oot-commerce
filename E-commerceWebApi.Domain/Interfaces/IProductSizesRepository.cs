
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IProductSizesRepository 
    {
        Task<ProductSizes> CreateAsync(ProductSizes entity);
        Task<ProductSizes> UpdateAsync(ProductSizes entity);
        Task<bool> DeleteAsync(int Id);
        Task<IEnumerable<ProductSizes>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
    }
}
