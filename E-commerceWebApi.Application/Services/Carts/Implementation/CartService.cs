using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Request;
using E_commerceWebApi.Application.DTOs.ShoppingCartDtos.Response;
using E_commerceWebApi.Application.Services.Carts.Interface;
using E_commerceWebApi.Application.Services.Coupons.Interface;
using E_commerceWebApi.Application.Services.Products.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;

namespace E_commerceWebApi.Application.Services.Carts.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly OnMapping _mapper;
        private readonly IUnitofWork _unitofWork;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;
        public CartService(ICartRepository repository, 
            OnMapping mapper, 
            IUnitofWork unitofWork, 
            IProductService productService,
            ICouponService couponService)
        {
            _repository = repository;
            _mapper = mapper;
            _unitofWork = unitofWork;
            _productService = productService;
            _couponService = couponService;
        }

        public async Task<Result<CartDto>> AddCartAsync(CartDto request, CancellationToken cancellationToken)
        {
            try
            {
                var cartHeader = await _repository.GetCartByUserIdAsync(request.CartResponse.UserId);

                if (cartHeader == null)
                {
                    var cartHeadermodel = await _mapper.Map<CartResponseDto, Cart>(request.CartResponse);

                    await _repository.CreateCartAsync(cartHeadermodel.Data);
                    await _unitofWork.SaveEntityChangesAsync();

                    request.CartItemResponse.First().CartId = cartHeadermodel.Data.Id;

                    var cartItemsmodel = await _mapper.Map<CartItemResponseDto, CartItem>(request.CartItemResponse.First());

                    await _repository.CreateCartItemAsync(cartItemsmodel.Data);
                    await _unitofWork.SaveEntityChangesAsync();

                    var mappedCartModel = new CartDto
                    {
                        CartResponse = new CartResponseDto
                        {
                            Id = cartItemsmodel.Data.CartId,
                            UserId = cartItemsmodel.Data.Cart.UserId,
                        },
                        CartItemResponse = new List<CartItemResponseDto>
                    {
                        new CartItemResponseDto
                        {
                             Id = cartItemsmodel.Data.Id,
                             CartId = cartItemsmodel.Data.CartId,
                             ProductId = cartItemsmodel.Data.ProductId,
                             Count = cartItemsmodel.Data.Count
                        }
                    }
                    };

                    return await Result<CartDto>.SuccessAsync(mappedCartModel, ResponseStatus.CreateSuccess, true);
                }

                else
                {
                    var cartItems = await _repository.GetCartItemByProductIdandCartId(u => u.ProductId == request.CartItemResponse.First().ProductId && u.CartId == cartHeader.Id);

                    if (cartItems == null)
                    {
                        request.CartItemResponse.First().CartId = cartHeader.Id;
                        var cartItemsmodel = await _mapper.Map<CartItemResponseDto, CartItem>(request.CartItemResponse.First());
                        await _repository.CreateCartItemAsync(cartItemsmodel.Data);
                        await _unitofWork.SaveEntityChangesAsync();


                        var mappedCartModel = new CartDto
                        {
                            CartResponse = new CartResponseDto
                            {
                                Id = cartItemsmodel.Data.CartId,
                                UserId = cartHeader.UserId,
                            },
                            CartItemResponse = new List<CartItemResponseDto>
                            {
                                new CartItemResponseDto
                                {
                                     Id = cartItemsmodel.Data.Id,
                                     CartId = cartItemsmodel.Data.CartId,
                                     ProductId = cartItemsmodel.Data.ProductId,
                                     Count = cartItemsmodel.Data.Count
                                }
                            }
                        };

                        return await Result<CartDto>.SuccessAsync(mappedCartModel, ResponseStatus.CreateSuccess, true);
                    }
                    else
                    {
                        request.CartItemResponse.First().Count += cartItems.Count;
                        request.CartItemResponse.First().CartId = cartItems.CartId;
                        request.CartItemResponse.First().Id = cartItems.Id;
                        var cartDetailsmodel = await _mapper.Map<CartItemResponseDto, CartItem>(request.CartItemResponse.First());

                        await _repository.UpdateCartItemAsync(cartDetailsmodel.Data);
                        await _unitofWork.SaveEntityChangesAsync();

                        var mappedCartModel = new CartDto
                        {
                            CartResponse = new CartResponseDto
                            {
                                Id = cartDetailsmodel.Data.CartId,
                                UserId = cartHeader.UserId,
                            },
                            CartItemResponse = new List<CartItemResponseDto>
                            {
                                new CartItemResponseDto
                                {
                                     Id = cartDetailsmodel.Data.Id,
                                     CartId = cartDetailsmodel.Data.CartId,
                                     ProductId = cartDetailsmodel.Data.ProductId,
                                     Count = cartDetailsmodel.Data.Count
                                }
                            }
                        };

                        return await Result<CartDto>.SuccessAsync(mappedCartModel, ResponseStatus.CreateSuccess, true);
                    }

                }

            }
            catch 
            {

                return await Result<CartDto>.FaildAsync(false, ResponseStatus.Faild);
            }
        }

        public async Task<Result<bool>> ApplyCartCoupon(CartDto request, CancellationToken cancellationToken)
        {
            try
            {
                var cartHeader = await _repository.GetCartByUserIdAsync(request.CartResponse.UserId);
                cartHeader.CouponCode = request.CartResponse.CouponCode;

                await _repository.UpdateCartAsync(cartHeader);
                await _unitofWork.SaveEntityChangesAsync();
                return await Result<bool>.SuccessAsync(true, ResponseStatus.AppliedSuccess, true);

            }
            catch
            {

                return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
            }
        }

        public async Task<Result<bool>> DeleteCartAsync(int cartItemid, CancellationToken cancellationToken)
        {
            try
            {
                var cartDetails = await _repository.GetCartItemById(cartItemid);

                int totalCountofCartItem = await _repository.GettotalCountofCartItem(cartDetails.CartId);

                await _repository.DeleteCartItemAsync(cartDetails);

                if (totalCountofCartItem == 1)
                {
                    var cartHeadertoGetReomved = await _repository.GetCartByIdAsync(cartDetails.CartId);
                    await _repository.DeleteCartAsync(cartHeadertoGetReomved);
                }

                await _unitofWork.SaveEntityChangesAsync();
                return await Result<bool>.SuccessAsync(true, ResponseStatus.DeletedSuccess, true);

            }
            catch
            {
                return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
            }
        }

        public async Task<Result<IEnumerable<CartDto>>> GetAllCartsAsync(string userId, CancellationToken cancellationToken)
        {
            var cartHeader = await _repository.GetCartByUserIdAsync(userId);

            if (cartHeader == null)
            {
                return await Result<IEnumerable<CartDto>>.FaildAsync(false, "Faild to return the UserId Carts");
            }

            var mappedcartHeaderResponse = await _mapper.Map<Cart, CartResponseDto>(cartHeader);

            var cart = new List<CartDto>
            {
                new CartDto
                {
                    CartResponse = mappedcartHeaderResponse.Data,
                }
            };

            var cartDetail = await _repository.GetCartItemByCartId(cart.First().CartResponse.Id);
            var mappedcartDetailsResponse = await _mapper.MapCollection<CartItem, CartItemResponseDto>(cartDetail);

            cart.First().CartItemResponse = mappedcartDetailsResponse.Data;

            var products = await _productService.GetAllAsync(cancellationToken);

            foreach (var item in cart.First().CartItemResponse)
            {
                item.Product = products.Data.FirstOrDefault(u => u.Id == item.ProductId);
                cart.First().CartResponse.CartTotal += (item.Count * item.Product.Price);
            };

            //Apply if there is any coupons

            if (!string.IsNullOrEmpty(cart.First().CartResponse.CouponCode))
            {
                var coupon = await _couponService.GetByCodeAsync(cart.First().CartResponse.CouponCode);
                if (coupon != null && cart.First().CartResponse.CartTotal > Convert.ToDouble(coupon.Data.MinAmount))
                {
                    cart.First().CartResponse.CartTotal -= Convert.ToDouble(coupon.Data.DiscountAmount);
                    cart.First().CartResponse.Discount = Convert.ToDouble(coupon.Data.DiscountAmount);
                }
                else
                {
                    return await Result<IEnumerable<CartDto>>.SuccessAsync(cart, ResponseStatus.GetAllSuccess, true);
                }
            }

            return await Result<IEnumerable<CartDto>>.SuccessAsync(cart, ResponseStatus.GetAllSuccess, true);
        }

        public Task<Result<CartResponseDto>> GetCartByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CartResponseDto>> GetCartByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> RemoveCartCoupon(CartDto request, CancellationToken cancellationToken)
        {
            try
            {
                var cartHeader = await _repository.GetCartByUserIdAsync(request.CartResponse.UserId);
                cartHeader.CouponCode = "";

                await _repository.UpdateCartAsync(cartHeader);
                await _unitofWork.SaveEntityChangesAsync();

                return await Result<bool>.SuccessAsync(true, ResponseStatus.RemovedSuccess, true);
            }
            catch
            {
                return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
            }
        }

        public Task<Result<CartResponseDto>> UpdateCartAsync(CartDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
