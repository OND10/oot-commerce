using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface ICouponRepository 
    {
        Task<Coupon> CreateAsync(Coupon model);
        Task<IEnumerable<Coupon>> GetAllAsync();
        Task<Coupon> GetByIdAsync(int id);
        Task<Coupon> GetByCodeAsync(string code);
        Task<Coupon> UpdateAsync(Coupon model);
        Task<bool> DeleteAsync(int Id);
    }
}
