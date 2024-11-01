using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.ProductImagesDtos.Response
{
    public class ProductImagesResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageExtension { get; set; }
        public bool IsMainImage { get; set; }
    }
}
