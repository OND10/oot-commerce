using E_commerceWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.RateDtos.Response
{
    public class RateResponseDto
    {
        public int? rate { get; set; }
        public string? Message { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        
    }
}
