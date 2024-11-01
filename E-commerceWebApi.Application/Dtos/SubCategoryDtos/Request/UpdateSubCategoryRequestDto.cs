using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.SubCategoryDtos.Request
{
    public class UpdateSubCategoryRequestDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
    }
}
