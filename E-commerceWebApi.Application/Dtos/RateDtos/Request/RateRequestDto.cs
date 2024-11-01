using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.RateDtos.Request
{
    public class RateRequestDto
    {
        [Range(1, 5)]
        public int? Rate { get; set; }
        public string? Message { get; set; }
        public int ProductId { get; set; }
        public string userId {  get; set; }
    }
}
