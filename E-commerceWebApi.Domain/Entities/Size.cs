using E_commerceWebApi.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Entities
{
    public class Size : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code {  get; set; } = string.Empty;
        public bool Active { get; set; }

    }
}
