using E_commerceWebApi.Domain.Common.Exceptions;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.EntityFrameworkCore;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class ProductSizesRepository : IProductSizesRepository
    {
        private readonly AppDbContext _context;

        public ProductSizesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductSizes> CreateAsync(ProductSizes entity)
        {

            var result = await _context.AddAsync<ProductSizes>(entity);

            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }

            throw new ModelNullException($"{entity}", "Model is null");
        }


        public async Task<bool> DeleteAsync(int Id)
        {
            var productSize = await GetByIdAsync(Id);

            if (productSize is not null)
            {
                _context.Remove(productSize);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ProductSizes>> GetAllAsync()
        {
            var result = await _context.ProductSizes.ToListAsync();

            if (result.Count > 0)
            {
                return result;
            }

            return Enumerable.Empty<ProductSizes>();
        }

        public async Task<ProductSizes> GetByIdAsync(int id)
        {
            var result = await _context.ProductSizes.FindAsync(id);

            if (result is not null)
            {
                return result;
            }
            else
            {
                throw new IdNullException(nameof(id));
            }
        }

        public async Task<IEnumerable<ProductSizes>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var result = await _context.ProductSizes.Where(p => p.ProductId == productId).ToListAsync();

            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                throw new IdNullException(nameof(productId));
            }
        }

        public async Task<ProductSizes> UpdateAsync(ProductSizes entity)
        {
            //var findId = await _context.ProductSizes.Include(x => x.Categories).FirstOrDefaultAsync(c => c.Id == model.Id);
            var findId = await _context.ProductSizes.Where(ps=>ps.ProductId == entity.ProductId).FirstOrDefaultAsync();
            if (findId != null)
            {
                // Update the blogpost
                _context.Entry(findId).CurrentValues.SetValues(entity);
                // Update the category
                //findId.Categories = model.Categories;

                return entity;
            }
            else
            {
                throw new ModelNullException($"{entity}", "Model is null");
            }
        }
    }
}
