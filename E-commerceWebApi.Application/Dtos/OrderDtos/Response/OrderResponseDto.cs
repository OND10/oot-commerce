using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request;
using E_commerceWebApi.Domain.Common.Enums;
using E_commerceWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Dtos.OrderDtos.Response
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double OrderTotal { get; set; }
        public string? EnglishName { get; set; }
        public string? ArabicName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public List<OrderItemResponseDto> OrderItems { get; set; }



        public OrderResponseDto ToResponse(CartDto cartDto)
        {
            var response = new OrderResponseDto
            {
                UserId = cartDto.CartResponse.UserId,
                CouponCode = cartDto.CartResponse.CouponCode,
                Discount = cartDto.CartResponse.Discount,
                OrderTotal = cartDto.CartResponse.CartTotal,
                ArabicName = cartDto.CartResponse.ArabicFullName,
                EnglishName = cartDto.CartResponse.EnglishFullName,
                Email = cartDto.CartResponse.Email,
                PhoneNumber = cartDto.CartResponse.PhoneNumber,
                OrderTime = DateTime.Now,
                Status = Order_Status.Pending.ToString(),
                OrderItems = new List<OrderItemResponseDto>()
            };

            return response;
        }


        public Order ToModel(OrderResponseDto response)
        {
            return new Order
            {
                UserId = response.UserId,
                CouponCode = response.CouponCode,
                Discount = response.Discount,
                OrderTotal = response.OrderTotal,
                ArabicName = response.ArabicName,
                EnglishName = response.EnglishName,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Status = response.Status,
                OrderTime = response.OrderTime,
                OrderItems = new List<OrderItem>() // Change to List
            };
        }
    }
}
