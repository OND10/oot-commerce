using E_commerceWebApi.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Entities
{
    public class OrderItem : AuditableEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public Product Product { get; set; }
        public int Count { get; set; }
        public string EnglishProductName { get; set; }
        public string ArabicProductName { get; set; }
        public float Price { get; set; }
    }
}
