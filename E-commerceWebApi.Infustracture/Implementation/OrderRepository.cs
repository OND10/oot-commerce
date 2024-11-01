using E_commerceWebApi.Domain.Common.Exceptions;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.EntityFrameworkCore;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Order> CreateAsync(Order entity)
        {
            //var result = await _context.Orders.AddAsync(entity);
            try
            {
                var result = _context.Orders.Add(entity).Entity;

                return Task.FromResult<Order>(result);
            }
            catch
            {
                throw new ModelNullException($"{entity}", $"{entity} is null");
            }
        }

        public Task<Order> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> FindAllAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Order> FindAsync(Expression<Func<Order, bool>> expression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<R>> FindAsync<R>(Expression<Func<Order, R>> selector, Expression<Func<Order, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllAsync<R>(Expression<Func<Order, R>> selector, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orderList = await _context.Orders.Include(o => o.OrderItems).OrderByDescending(o => o.Id).ToListAsync();

            if(orderList.Count > 0)
            {
                return orderList;
            }

            return Enumerable.Empty<Order>();
        }

        public async Task<IEnumerable<Order>> GetAllUserOrdersAsync(string userId, CancellationToken cancellationToken)
        {
            var orderList = await _context.Orders.Include(o => o.OrderItems).Where(o => o.UserId == userId).OrderByDescending(o => o.Id).ToListAsync();

            if (orderList.Count > 0)
            {
                return orderList;
            }

            return Enumerable.Empty<Order>();
        }

        public async Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var orderHeader = await _context.Orders.FirstAsync(o => o.Id == id);

            if (orderHeader == null)
            {
                throw new IdNullException($"{orderHeader} is null");
            }

            return orderHeader;
        }

        public async Task<Order> GetByIdIncludeAsync(int id, CancellationToken cancellationToken)
        {
            var orderHeader = await _context.Orders.Include(o => o.OrderItems).FirstAsync(o => o.Id == id);

            if (orderHeader is not null)
            {
                return orderHeader;
            }

            throw new IdNullException($"{orderHeader} is null");
        }

        public Task<IEnumerable<Order>> QueryAsync(string query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
