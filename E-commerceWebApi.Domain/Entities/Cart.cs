using E_commerceWebApi.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Entities
{
    public class Cart : AuditableEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public double CartTotal { get; set; }
        //public ICollection<CartItem> CartItems { get; set; }
    }
}
