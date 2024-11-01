using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IProductColorsRepository 
    {
        Task<ProductColors>CreateAsync(ProductColors entity);
        Task<ProductColors> UpdateAsync(ProductColors entity);
        Task<bool> DeleteAsync(int Id);
        Task<IEnumerable<ProductColors>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
    }
}
