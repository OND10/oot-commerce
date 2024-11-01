using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _context;

        public UnitofWork(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<int> SaveEntityChangesAsync()
        {
            return await _context.SaveChangesAsync();

        }
    }
}
