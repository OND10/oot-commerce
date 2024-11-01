using E_commerceWebApi.Domain.Common.Exceptions;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _context;
        public CouponRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Coupon> CreateAsync(Coupon entity)
        {
            await _context.Coupons.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Coupon>> GetAllAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetByIdAsync(int id)
        {
            var result = await _context.Coupons.FindAsync(id);

            if (result is not null)
            {
                return result;
            }
            throw new IdNullException(nameof(result));
        }

        public async Task<Coupon> UpdateAsync(Coupon entity)
        {
            var findId = _context.Coupons.FirstOrDefault(c => c.Id == entity.Id);
            if (findId != null)
            {
                _context.Entry(findId).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            else
            {
                throw new ModelNullException($"{entity}", "Model is null");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Coupon = await _context.Coupons.FindAsync(id);
            if (Coupon == null) return false;

            _context.Coupons.Remove(Coupon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Coupon> GetByCodeAsync(string code)
        {
            var result = await _context.Coupons.Where(c=>c.CouponCode == code).FirstOrDefaultAsync();

            if (result is not null)
            {
                return result;
            }
            throw new IdNullException(nameof(result));
        }
    }
}
