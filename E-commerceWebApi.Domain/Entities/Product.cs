using E_commerceWebApi.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string EnglishName { get; set; } = string.Empty;
        public string ArabicName { get; set; } = string.Empty;
        public string EnglishDescription { get; set; } = string.Empty;
        public string ArabicDescription { get; set; } = string.Empty;
        public float Price { get; set; }
        public float DiscountPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public bool IsFeatured { get; set; }
        public int SubcategoryId { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId {  get; set; }

        public virtual Brand Brand { get; set; }    

    }
}
