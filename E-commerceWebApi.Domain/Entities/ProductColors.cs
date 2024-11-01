using E_commerceWebApi.Domain.Shared;

namespace E_commerceWebApi.Domain.Entities
{
    public class ProductColors : AuditableEntity
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public bool IsMainColor { get; set; }
    }
}
