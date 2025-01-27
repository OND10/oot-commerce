﻿using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request
{
    public class CartDto
    {
        public CartResponseDto? CartResponse { get; set; }
        public IEnumerable<CartItemResponseDto>? CartItemResponse { get; set; }
    }
}
