using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;
using E_commerceWebApi.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(int id);
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        //Task<Cart> AddCartAsync(CartDto entity);
        Task<Cart> UpdateCartAsync(Cart entity);
        Task<bool> DeleteCartAsync(Cart entity);
        Task<bool> DeleteCartItemAsync(CartItem entity);
        Task<Cart> CreateCartAsync(Cart entity);
        Task<CartItem> CreateCartItemAsync(CartItem entity);
        Task<CartItem> GetCartItemByProductIdandCartId(Expression<Func<CartItem, bool>> expression);
        Task<CartItem> GetCartItemById(int Id);
        Task<IQueryable<CartItem>> GetCartItemByCartId(int Id);
        Task<int> GettotalCountofCartItem(int cartId);

        Task<CartItem> UpdateCartItemAsync(CartItem entity);
        
    }
}
