using E_commerceWebApi.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Entities
{
    public class Rate : AuditableEntity
    {
        public int? rate { get; set; }
        public string? Message { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public ApplicationUser ApplicationUser{ get; set; }
        public Product Product { get; set; }
    }
}
