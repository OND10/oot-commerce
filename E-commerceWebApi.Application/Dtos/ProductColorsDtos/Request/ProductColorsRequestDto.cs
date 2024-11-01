using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.ProductColorsDtos.Request
{
    public class ProductColorsRequestDto
    {
        public int ProductId { get; set; }
        public List<int> ColorsId { get; set; }

        public bool IsMainColor { get; set; }
    }
}
