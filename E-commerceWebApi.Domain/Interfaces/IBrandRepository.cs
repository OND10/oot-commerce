using E_commerceWebApi.Domain.Entities;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IBrandRepository
    {
        Task<Brand> CreateAsync(Brand entity);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand> GetByIdAsync(int id);
        Task<Brand> UpdateAsync(Brand entity);
        Task<bool> DeleteAsync(int Id);
    }
}
