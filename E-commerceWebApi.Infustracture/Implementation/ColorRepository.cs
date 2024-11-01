using E_commerceWebApi.Domain.Common.Exceptions;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.EntityFrameworkCore;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class ColorRepository : IColorRepository
    {
        private readonly AppDbContext _context;
        public ColorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Color> CreateAsync(Color entity)
        {
            var result = await _context.Colors.AddAsync(entity);
            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }
            throw new ModelNullException(nameof(entity), "Model is null");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null) return false;

            _context.Colors.Remove(color);

            return true;
        }

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            return await _context.Colors.ToListAsync();
        }
    }
}
