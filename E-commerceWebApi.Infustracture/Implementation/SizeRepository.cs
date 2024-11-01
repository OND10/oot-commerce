using E_commerceWebApi.Domain.Common.Exceptions;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.EntityFrameworkCore;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class SizeRepository : ISizeRepository
    {
        private readonly AppDbContext _context;

        public SizeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Size> CreateAsync(Size entity)
        {
            var result = await _context.Sizes.AddAsync(entity);
            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }
            throw new ModelNullException(nameof(entity), "Model is null");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var size = await _context.Sizes.FindAsync(id);
            if (size == null) return false;

            _context.Sizes.Remove(size);

            return true;
        }

        public async Task<IEnumerable<Size>> GetAllAsync()
        {
            return await _context.Sizes.ToListAsync();
        }
    }
}
