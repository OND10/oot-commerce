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
    public class BrandRepository : IBrandRepository
    {

        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> CreateAsync(Brand entity)
        {
            var result = await _context.Brands.AddAsync(entity);
            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }
            throw new ModelNullException(nameof(entity), "Model is null");
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            var result = await _context.Brands.FindAsync(id);

            if (result is not null)
            {
                return result;
            }
            throw new IdNullException(nameof(result));
        }

        public async Task<Brand> UpdateAsync(Brand entity)
        {
            var findId = await _context.Brands.FirstOrDefaultAsync(c => c.Id == entity.Id);
            if (findId != null)
            {
                _context.Entry(findId).CurrentValues.SetValues(entity);

                return entity;
            }
            else
            {
                throw new ModelNullException($"{entity}", "Model is null");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Brand = await _context.Brands.FindAsync(id);
            if (Brand == null) return false;

            _context.Brands.Remove(Brand);

            return true;
        }

    }
}
