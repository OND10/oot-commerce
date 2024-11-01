using E_commerceWebApi.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
        public int? ParentCategoryId { get; set; }
        public IEnumerable<Subcategory> Subcategories{ get; set;}

    }
}
