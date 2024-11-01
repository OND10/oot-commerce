using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response
{
    public class ProductSizesResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
    }
}
