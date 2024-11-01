using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<Subcategory> CreateAsync(Subcategory entity);
        Task<IEnumerable<Subcategory>> GetAllAsync();
        Task<Subcategory> GetByIdAsync(int id);
        Task<Subcategory> GetByCategoryIdAsync(int categoryId);
        Task<Subcategory> UpdateAsync(Subcategory entity);
        Task<bool> DeleteAsync(int Id);
    }
}
