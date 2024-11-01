using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IUnitofWork
    {
        Task<int> SaveEntityChangesAsync();
    }
}
