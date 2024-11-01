using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Shared
{
    public class AuditableEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; } = string.Empty;   
        public DateTime? DeletedAt { get; set; }
    }
}
