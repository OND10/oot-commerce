using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ColorDtos.Request;
using E_commerceWebApi.Application.Dtos.ColorDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductColorsDtos.Response;
using E_commerceWebApi.Application.Services.Colors.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _service;
        public ColorsController(IColorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<ColorResponseDto>>> Get(CancellationToken cancellationToken)
        {
            var response = await _service.GetAllAsync();

            if (response.IsSuccess)
            {
                return await Result<IEnumerable<ColorResponseDto>>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<IEnumerable<ColorResponseDto>>.FaildAsync(false, ResponseStatus.Faild);

        }

        [HttpPost]
        public async Task<Result<ColorResponseDto>> Post(ColorRequestDto request)
        {
            var response = await _service.CreateAsync(request);

            if (response.IsSuccess)
            {
                return await Result<ColorResponseDto>.SuccessAsync(response.Data, ResponseStatus.CreateSuccess, true);
            }

            return await Result<ColorResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpDelete]
        public async Task<Result<bool>> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, ResponseStatus.DeletedSuccess, true);
            }

            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);

        }
    }
}
