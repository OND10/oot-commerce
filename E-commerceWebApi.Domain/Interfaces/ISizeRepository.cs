using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface ISizeRepository
    {
        Task<Size> CreateAsync(Size entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Size>> GetAllAsync();
    }
}
