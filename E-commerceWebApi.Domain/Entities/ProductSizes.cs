using E_commerceWebApi.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Entities
{
    public class ProductSizes : AuditableEntity
    {
        public int ProductId {  get; set; }
        public int SizeId { get; set; }

    }
}
