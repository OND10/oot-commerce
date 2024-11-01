using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<CartItem> GetCartItemByIdAsync(int id);
        Task<IEnumerable<CartItem>> GetCartItemByCartHeaderIdAsync(int cartHeaderId);
        Task<IEnumerable<CartItem>> GetAllCartItemAsync();
        Task<CartItem> AddCartItemAsync(CartItem entity);
        Task<CartItem> UpdateCartItemAsync(CartItem entity);
        Task<bool> DeleteCartItemAsync(int id);
        Task<bool> DeleteCartItemByCartHeaderIdAsync(int cartHeaderId);
    }
}
