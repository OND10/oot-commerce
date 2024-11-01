using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.OrderDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request;
using E_commerceWebApi.Application.Services.Orders.Interface;
using E_commerceWebApi.Domain.Common.Enums;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using Stripe;

namespace E_commerceWebApi.Application.Services.Orders.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitofWork _unitofWork;
        public OrderService(IOrderRepository repository, IUnitofWork unitofWork)
        {
            _repository = repository;
            _unitofWork = unitofWork;
        }


        public async Task<Result<OrderResponseDto>> CreateOrderAsync(CartDto request, CancellationToken cancellationToken)
        {
            try
            {
                var orderResponse = new OrderResponseDto();

                var mappedOrderResponse = orderResponse.ToResponse(request);

                foreach (var cartItem in request.CartItemResponse)
                {
                    var orderDetail = new OrderItemResponseDto
                    {
                        OrderId = mappedOrderResponse.Id,
                        ProductId = cartItem.ProductId,
                        Count = cartItem.Count,
                        Product = cartItem.Product,
                        ArabicProductName = cartItem.Product.ArabicName,
                        EnglishProductName = cartItem.Product.EnglishName,
                        Price = cartItem.Product.Price
                    };
                    mappedOrderResponse.OrderItems.Add(orderDetail); // Use Add
                }

                var mappedtoOrderModel = orderResponse.ToModel(mappedOrderResponse);

                foreach (var orderItemResponse in mappedOrderResponse.OrderItems)
                {
                    var productResponse = new ProductResponseDto();
                    var productModel = productResponse.ToModel(orderItemResponse.Product);
                    var orderDetail = new OrderItem
                    {
                        OrderId = mappedtoOrderModel.Id,
                        ProductId = orderItemResponse.ProductId,
                        Count = orderItemResponse.Count,
                        Product = productModel,
                        ArabicProductName = orderItemResponse.ArabicProductName,
                        EnglishProductName = orderItemResponse.EnglishProductName,
                        Price = orderItemResponse.Price
                    };
                    mappedtoOrderModel.OrderItems.Add(orderDetail); // Use Add
                }

                var create = await _repository.CreateAsync(mappedtoOrderModel);
                await _unitofWork.SaveEntityChangesAsync();

                mappedOrderResponse.Id = create.Id;

                return await Result<OrderResponseDto>.SuccessAsync(mappedOrderResponse, ResponseStatus.CreateSuccess, true);
            }
            catch (Exception ex)
            {
                return await Result<OrderResponseDto>.FaildAsync(false, $"{ex.Message}");
            }
        }
        public async Task<Result<IEnumerable<OrderResponseDto>>> GetAllOrder(string userId, string role, CancellationToken cancellationToken)
        {
            var result = default(Result<IEnumerable<OrderResponseDto>>);

            string[] validProcesses = { "Admin", "User" };
            var obj = new Dictionary<string, Func<Task<Result<IEnumerable<OrderResponseDto>>>>>()
            {
                    {"Admin", async() => await GetAllOrders(cancellationToken)},
                    {"User", async() => await GetAllUserOrders(userId, cancellationToken)}
            };

            result = await obj[role]();

            return await Result<IEnumerable<OrderResponseDto>>.SuccessAsync(result.Data, result.Message, true);
        }
        private async Task<Result<IEnumerable<OrderResponseDto>>> GetAllOrders(CancellationToken cancellationToken)
        {

            var orderHeaderDtoList = new List<OrderResponseDto>();
            var orderDetailsList = new List<OrderItemResponseDto>();

            var orderHeaderList = await _repository.GetAllOrdersAsync(cancellationToken);

            foreach (var orderHeader in orderHeaderList)
            {

                foreach (var item in orderHeader.OrderItems)
                {

                    var productResponse = new ProductResponseDto();
                    var productModel = productResponse.ToResponse(item.Product);
                    var orderDetails = new OrderItemResponseDto
                    {
                        Id = item.Id,
                        Count = item.Count,
                        OrderId = item.OrderId,
                        Price = item.Price,
                        Product = productModel,
                        ProductId = item.ProductId,
                        ArabicProductName = item.ArabicProductName,
                        EnglishProductName = item.EnglishProductName,
                    };
                    orderDetailsList.Add(orderDetails);
                }

                var orderResponse = new OrderResponseDto
                {
                    Id = orderHeader.Id,
                    CouponCode = orderHeader.CouponCode,
                    Discount = orderHeader.Discount,
                    Email = orderHeader.Email,
                    ArabicName = orderHeader.ArabicName,
                    EnglishName = orderHeader.EnglishName,
                    OrderTime = orderHeader.OrderTime,
                    OrderTotal = orderHeader.OrderTotal,
                    PhoneNumber = orderHeader.PhoneNumber,
                    Status = orderHeader.Status,
                    UserId = orderHeader.UserId,
                    OrderItems = orderDetailsList
                };

                orderHeaderDtoList.Add(orderResponse);
            }


            return await Result<IEnumerable<OrderResponseDto>>.SuccessAsync(orderHeaderDtoList, "GetAll Orders Successfully", true);
        }
        private async Task<Result<IEnumerable<OrderResponseDto>>> GetAllUserOrders(string? userId = "", CancellationToken cancellationToken = default)
        {
            var orderHeaderDtoList = new List<OrderResponseDto>();
            var orderDetailsList = new List<OrderItemResponseDto>();

            var orderHeaderList = await _repository.GetAllUserOrdersAsync(userId, cancellationToken);

            orderHeaderDtoList = new List<OrderResponseDto>();

            orderDetailsList = new List<OrderItemResponseDto>();


            foreach (var orderHeader in orderHeaderList)
            {

                foreach (var item in orderHeader.OrderItems)
                {
                    var productResponse = new ProductResponseDto();
                    var productModel = productResponse.ToResponse(item.Product);
                    var orderDetails = new OrderItemResponseDto
                    {
                        Id = item.Id,
                        Count = item.Count,
                        OrderId = item.OrderId,
                        Price = item.Price,
                        Product = productModel,
                        ProductId = item.ProductId,
                        ArabicProductName = item.ArabicProductName,
                        EnglishProductName = item.EnglishProductName,
                    };

                    orderDetailsList.Add(orderDetails);
                }

                var orderResponse = new OrderResponseDto
                {
                    Id = orderHeader.Id,
                    CouponCode = orderHeader.CouponCode,
                    Discount = orderHeader.Discount,
                    Email = orderHeader.Email,
                    ArabicName = orderHeader.ArabicName,
                    EnglishName = orderHeader.EnglishName,
                    OrderTime = orderHeader.OrderTime,
                    OrderTotal = orderHeader.OrderTotal,
                    PhoneNumber = orderHeader.PhoneNumber,
                    Status = orderHeader.Status,
                    UserId = orderHeader.UserId,
                    OrderItems = orderDetailsList
                };

                orderHeaderDtoList.Add(orderResponse);
            }

            return await Result<IEnumerable<OrderResponseDto>>.SuccessAsync(orderHeaderDtoList, "GetAll User Orders Successfully", true);

        }
        public async Task<Result<OrderResponseDto>> GetOrderById(int orderId, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdIncludeAsync(orderId, cancellationToken);

            var orderDetailsList = new List<OrderItemResponseDto>();

            foreach(var item in order.OrderItems)
            {

                var productResponse = new ProductResponseDto();
                var productModel = productResponse.ToResponse(item.Product);

                var orderItems = new OrderItemResponseDto
                {
                    Id = item.Id,
                    Count = item.Count,
                    OrderId = item.OrderId,
                    Price = item.Price,
                    Product = productModel,
                    ProductId = item.ProductId,
                    EnglishProductName = item.EnglishProductName,
                    ArabicProductName = item.ArabicProductName,
                };
                orderDetailsList.Add(orderItems);
            }

            var orderResponse = new OrderResponseDto
            {
                Id = order.Id,
                CouponCode = order.CouponCode,
                Discount = order.Discount,
                Email = order.Email,
                EnglishName = order.EnglishName,
                ArabicName = order.ArabicName,
                OrderTime = order.OrderTime,
                OrderTotal = order.OrderTotal,
                PhoneNumber = order.PhoneNumber,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = orderDetailsList
            };

            return await Result<OrderResponseDto>.SuccessAsync(orderResponse, "GetAll Orders Successfully", true);
        }
        public async Task<Result<bool>> UpdateOrderStatusAsync(int orderId, string newStatus, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(orderId, cancellationToken);

            if(newStatus == Order_Status.Cancelled.ToString())
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    //PaymentIntent = order.PaymentIntentId,

                };

                order.Status = newStatus;

                await _unitofWork.SaveEntityChangesAsync();

                return await Result<bool>.SuccessAsync(true, "OrderStatus is updated Successfully", true);
            }

            return await Result<bool>.FaildAsync(false, "OrderStatus is not updated");
        }
    }
}
