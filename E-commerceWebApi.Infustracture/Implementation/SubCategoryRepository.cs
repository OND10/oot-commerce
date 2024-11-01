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
    public class SubCategoryRepository : ISubcategoryRepository
    {

        private readonly AppDbContext _context;
        public SubCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subcategory> CreateAsync(Subcategory entity)
        {

            var result = await _context.Subcategories.AddAsync(entity);
            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }
            throw new ModelNullException(nameof(entity), "Model is null");
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var subCategory = await _context.Categories.FindAsync(id);
            if (subCategory == null) return false;

            _context.Categories.Remove(subCategory);

            return true;
        }

        public async Task<IEnumerable<Subcategory>> GetAllAsync()
        {
            return await _context.Subcategories.ToListAsync();
        }

        public async Task<Subcategory> GetByCategoryIdAsync(int categoryId)
        {
            var result = await _context.Subcategories.Where(s => s.CategoryId == categoryId).FirstOrDefaultAsync();

            if (result is not null)
            {
                return result;
            }
            throw new IdNullException(nameof(result));
        }

        public async Task<Subcategory> GetByIdAsync(int id)
        {
            var result = await _context.Subcategories.FindAsync(id);

            if (result is not null)
            {
                return result;
            }
            throw new IdNullException(nameof(result));
        }

        public async Task<Subcategory> UpdateAsync(Subcategory entity)
        {
            var findId = await _context.Subcategories.FirstOrDefaultAsync(c => c.Id == entity.Id);
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
    }
}
