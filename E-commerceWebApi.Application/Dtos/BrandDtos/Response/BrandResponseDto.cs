﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.BrandDtos.Response
{
    public class BrandResponseDto
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
    }
}
