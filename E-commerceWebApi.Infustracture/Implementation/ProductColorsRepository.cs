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
    public class ProductColorsRepository : IProductColorsRepository
    {
        private readonly AppDbContext _context;
        public ProductColorsRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ProductColors> CreateAsync(ProductColors entity)
        {
            var result = await _context.AddAsync<ProductColors>(entity);

            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }

            throw new ModelNullException($"{entity}", "Model is null");
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var result = await _context.ProductColors.FindAsync(Id);
            if (result == null) return false;

            _context.ProductColors.Remove(result);

            return true;
        }

        public async Task<IEnumerable<ProductColors>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var result = await _context.ProductColors.Where(p => p.ProductId == productId).ToListAsync();

            if(result is not null)
            {
                return result;
            }

            throw new IdNullException(nameof(result));
        }

        public async Task<ProductColors> UpdateAsync(ProductColors entity)
        {
            var findId = await _context.ProductColors.FindAsync(entity.Id);
            if (findId != null)
            {
                // Update the blogpost
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
