using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Response;
using E_commerceWebApi.Domain.Common.Exceptions;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateCartAsync(Cart entity)
        {
            var result = await _context.Carts.AddAsync(entity);

            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }

            throw new ModelNullException(nameof(entity), $"{entity} is null");
        }

        public async Task<CartItem> CreateCartItemAsync(CartItem entity)
        {
            var result = await _context.CartItems.AddAsync(entity);

            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }

            throw new ModelNullException(nameof(entity), $"{entity} is null");
        }

        
        public Task<bool> DeleteCartAsync(Cart entity)
        {
            _context.Remove(entity);

            return Task.FromResult<bool>(true);
        }

        public Task<bool> DeleteCartItemAsync(CartItem entity)
        {
            _context.Remove(entity);

            return Task.FromResult<bool>(true);
        }

        public Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            var cartHeadertoGetReomved = await _context.Carts.FirstOrDefaultAsync(u => u.Id == id);

            if(cartHeadertoGetReomved != null)
            {
                return cartHeadertoGetReomved;
            }

            throw new IdNullException($"{cartHeadertoGetReomved} is null");
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            var cartHeader = await _context.Carts.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);

            if (cartHeader is not null)
            {
                return cartHeader;
            }
            return null;
        }

        public Task<IQueryable<CartItem>> GetCartItemByCartId(int Id)
        {
            var cartDetail = _context.CartItems.Where(u => u.CartId == Id);

            if( cartDetail.Any() ) 
            {
                return Task.FromResult<IQueryable<CartItem>>(cartDetail);
            }
            return Task.FromResult(Queryable.DefaultIfEmpty<CartItem>(cartDetail));
        }

        public async Task<CartItem> GetCartItemById(int Id)
        {
            var cartDetails = await _context.CartItems.FirstAsync(u => u.Id == Id);

            if(cartDetails is not null)
            {
                return cartDetails;
            }

            throw new IdNullException($"{cartDetails} is null");
        }

        public async Task<CartItem> GetCartItemByProductIdandCartId(Expression<Func<CartItem, bool>> expression)
        {
            var result = await _context.CartItems.AsNoTracking().FirstOrDefaultAsync(expression);

            if(result is not null)
            {
                return result;
            }

            return null;
        }

        public async Task<int> GettotalCountofCartItem(int cartId)
        {
            int totalCountofCartItem = await _context.CartItems.Where(u => u.CartId == cartId).CountAsync();

            if(totalCountofCartItem > 0)
            {
                return totalCountofCartItem;
            }

            throw new IdNullException($"{cartId} is null");
        }

        public Task<Cart> UpdateCartAsync(Cart entity)
        {
            var result = _context.Update(entity);

            if (result.State == EntityState.Modified)
            {
                return Task.FromResult<Cart>(result.Entity);
            }

            throw new ModelNullException($"{entity}", $"{entity} is null");
        }

        public Task<CartItem> UpdateCartItemAsync(CartItem entity)
        {
            var result = _context.Update(entity);

            if(result.State == EntityState.Modified)
            {
                return Task.FromResult<CartItem>(result.Entity);
            }

            throw new ModelNullException($"{entity}", $"{entity} is null");
        }
    }
}
