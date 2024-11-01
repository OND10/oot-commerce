using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.RateDtos.Request;
using E_commerceWebApi.Application.Dtos.RateDtos.Response;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using OnMapper;

namespace E_commerceWebApi.Application.Services.Rate
{
    public class RateService : IRateService
    {
        private readonly IRateRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitofWork _unitofWork;
        private readonly OnMapping _mapper;
        public RateService(IRateRepository repository, IProductRepository productRepository, IUnitofWork unitofWork, OnMapping mapper) 
        {
            _repository = repository;
            _productRepository = productRepository;
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<Result<RateResponseDto>> AddRate(RateRequestDto request)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if(product is null)
            {
                return await Result<RateResponseDto>.FaildAsync(false, "Invalid Product.. Please Try Again !");
            }

            var rate = new Domain.Entities.Rate
            {
                UserId = request.userId,
                ProductId = product.Id,
                rate = request.Rate,
                Message = request.Message
            };

            await _repository.AddAsync(rate);
            await _unitofWork.SaveEntityChangesAsync();

            var mappedResponse = await _mapper.Map<Domain.Entities.Rate, RateResponseDto>(rate);

            return await Result<RateResponseDto>.SuccessAsync(mappedResponse.Data, ResponseStatus.CreateSuccess, true);
        }

        public async Task<Result<double>> GetavarageForProduct(int productId)
        {
            var rate = await _repository.GetavarageForProduct(u=> u.ProductId == productId);

            var result = rate.Any() ? (double)rate.Average(r => r.rate) : 0;

            return await Result<double>.SuccessAsync(result, ResponseStatus.GetSuccess, true);
        }

        public async Task<Result<ProductRateResponseDto>> GetProductsRate(int productId)
        {
            var rate = await _repository.GetProductsRate(c => c.ProductId == productId, new[] { "Product", "User" });

            var count = rate.Count();
            var productRate = new ProductRateResponseDto { Count = count };
            decimal? sum = 0;

            var userRate = new List<UserRateDto>();

            if (count == 0)
            {
                productRate.Count = 0;
                productRate.Avarage = 0;

                return await Result<ProductRateResponseDto>.FaildAsync(false, "There is no rates for this Product");
            }

            foreach (var item in rate)
            {
                var result = new UserRateDto
                {
                    Rate = item.rate,
                    Comment = item.Message,
                    UserName = item.ApplicationUser.UserName
                };
                sum += result.Rate;
                userRate.Add(result);
            }

            var Avarage = sum / productRate.Count;
            productRate.Avarage = Avarage;
            productRate.CustomerRate = userRate;

            return await Result<ProductRateResponseDto>.SuccessAsync(productRate, ResponseStatus.GetSuccess, true);
        }
    }
}
