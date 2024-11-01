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
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)      
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var result = await _context.Products.FindAsync(id);
            
            if(result is not null)
            {
                return result;
            }
            throw new IdNullException(nameof(result));
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            var findId = _context.Products.FirstOrDefault(c => c.Id == entity.Id);
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
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

    }


}
