using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IColorRepository 
    {
        Task<Color> CreateAsync(Color entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Color>> GetAllAsync();
    }
}
