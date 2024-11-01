using E_commerceWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.OrderDtos.Request
{
    public class OrderRequestDto
    {
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public float Discount { get; set; }
        public float OrderTotal { get; set; }
        public string? EnglishName { get; set; }
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
