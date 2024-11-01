using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IProductRepository 
    {
        Task<Product> CreateAsync(Product entity);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> UpdateAsync(Product entity);
        Task<bool> DeleteAsync(int Id);
    }
}
