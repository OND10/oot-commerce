using E_commerceWebApi.Domain.Common.Exceptions;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class RateRepository : IRateRepository
    {
        private readonly AppDbContext _context;
        public RateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rate> AddAsync(Rate entity)
        {
            var result = await _context.AddAsync(entity);

            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }

            throw new ModelNullException($"{entity}", "Model is null");
        }

        public async Task<IEnumerable<Rate>> GetavarageForProduct(Expression<Func<Rate, bool>> match)
        {
            var result = await _context.Rates.Where(match).ToListAsync();

            if (result.Any())
            {
                return result;
            }

            return Enumerable.Empty<Rate>();
        }

        public async Task<IEnumerable<Rate>> GetProductsRate(Expression<Func<Rate, bool>> match, string[] include = null!)
        {
            IQueryable<Rate> query = _context.Set<Rate>();

            if (query == null)
                return null;
            if (include != null)
                foreach (var item in include)
                    query = query.Include(item);

            return await query.Where(match).ToListAsync();
        }
    }
}
