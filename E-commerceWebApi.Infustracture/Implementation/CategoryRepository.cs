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
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category entity)
        {
            var result = await _context.Categories.AddAsync(entity);
            if(result.State == EntityState.Added)
            {
                return result.Entity;
            }
            throw new ModelNullException(nameof(entity), "Model is null");
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var result = await _context.Categories.FindAsync(id);

            if (result is not null)
            {
                return result;
            }
            throw new IdNullException(nameof(result));
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            var findId = _context.Categories.FirstOrDefault(c => c.Id == entity.Id);
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
            var Category = await _context.Categories.FindAsync(id);
            if (Category == null) return false;

            _context.Categories.Remove(Category);
            
            return true;
        }

    }
}
