using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.ProductImagesDtos.Request
{
    public class ProductImagesRequestDto
    {
        public Guid ProductId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageExtension { get; set; }
        public bool IsMainImage { get; set; }
    }
}
