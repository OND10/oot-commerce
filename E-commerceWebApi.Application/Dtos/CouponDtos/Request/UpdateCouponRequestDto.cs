using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.CouponDtos.Request
{
    public class UpdateCouponRequestDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
        public decimal MinAmount { get; set; }
        public string? StripeCouponId { get; set; }
    }
}
