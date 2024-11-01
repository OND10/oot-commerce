using E_commerceWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IRateRepository
    {
        Task<Rate> AddAsync(Rate entity);
        Task<IEnumerable<Rate>> GetProductsRate(Expression<Func<Rate, bool>> match, string[] include = null!);
        Task<IEnumerable<Rate>> GetavarageForProduct(Expression<Func<Rate, bool>> match);
    }
}
