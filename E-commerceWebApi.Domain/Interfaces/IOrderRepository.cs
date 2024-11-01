using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync<R>(Expression<Func<Order, R>> selector, CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetAllUserOrdersAsync(string userId, CancellationToken cancellationToken);
        Task<IEnumerable<Order>> FindAllAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken);
        Task<Order> FindAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken);
        Task<IEnumerable<R>> FindAsync<R>(Expression<Func<Order, R>> selector, Expression<Func<Order, bool>> expression);
        Task<Order> CreateAsync(Order entity);
        Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken);
        Task<Order> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Order> GetByIdIncludeAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Order>> QueryAsync(string query, CancellationToken cancellationToken);

    }
}
