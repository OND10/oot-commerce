using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;
using E_commerceWebApi.Application.Dtos.SizeDtos.Request;
using E_commerceWebApi.Application.Dtos.SizeDtos.Response;
using E_commerceWebApi.Application.Services.Sizes.Interface;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _service;
        
        public SizesController(ISizeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<SizeResponseDto>>> Get(CancellationToken cancellationToken)
        {
            var response = await _service.GetAllAsync();

            if (response.IsSuccess)
            {
                return await Result<IEnumerable<SizeResponseDto>>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<IEnumerable<SizeResponseDto>>.FaildAsync(false, ResponseStatus.Faild);

        }

        [HttpPost]
        public async Task<Result<SizeResponseDto>> Post(SizeRequestDto request)
        {
            var response = await _service.CreateAsync(request);

            if (response.IsSuccess)
            {   
                return await Result<SizeResponseDto>.SuccessAsync(response.Data, ResponseStatus.CreateSuccess, true);
            }

            return await Result<SizeResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpDelete]
        public async Task<Result<bool>> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);

        }
    }
}
