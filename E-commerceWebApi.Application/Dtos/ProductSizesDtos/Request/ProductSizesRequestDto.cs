using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.ProductSizesDtos.Request
{
    public class ProductSizesRequestDto
    {
        public int ProductId { get; set; }
        public List<int> SizesId { get; set; }
    }
}
