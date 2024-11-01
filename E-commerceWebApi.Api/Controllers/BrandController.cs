using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.BrandDtos.Request;
using E_commerceWebApi.Application.Dtos.BrandDtos.Response;
using E_commerceWebApi.Application.Services.Brands.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMapper;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;
        private readonly OnMapping _mapper;
        public BrandController(IBrandService service, OnMapping mapper) 
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<Result<IEnumerable<BrandResponseDto>>> Get(CancellationToken cancellationToken)
        {

            var result = await _service.GetAllAsync(cancellationToken);

            return await Result<IEnumerable<BrandResponseDto>>.SuccessAsync(result.Data, result.Message, true);
        }

        [HttpGet("{id}")]
        public async Task<Result<BrandResponseDto>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);

            return await Result<BrandResponseDto>.SuccessAsync(result.Data, result.Message, true);
        }

        [Authorize]
        [HttpPost]
        public async Task<Result<BrandResponseDto>> Post([FromBody] BrandRequestDto model, CancellationToken cancellationToken)
        {

            var result = await _service.CreateAsync(model, cancellationToken);

            if (result.IsSuccess)
            {
                return await Result<BrandResponseDto>.SuccessAsync(result.Data, result.Message, true);
            }

            return await Result<BrandResponseDto>.FaildAsync(true, ResponseStatus.Faild);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<Result<BrandResponseDto>> Put([FromRoute] int id, [FromBody] BrandRequestDto model, CancellationToken cancellationToken)
        {
            try
            {
                var mappedRequestModel = await _mapper.Map<BrandRequestDto, UpdateBrandRequestDto>(model);

                mappedRequestModel.Data.Id = id;

                var updateResult = await _service.UpdateAsync(mappedRequestModel.Data, cancellationToken);

                return await Result<BrandResponseDto>.SuccessAsync(updateResult.Data, ResponseStatus.UpdateSuccess, true);
            }
            catch (Exception)
            {
                return await Result<BrandResponseDto>.FaildAsync(true, ResponseStatus.Faild);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var delete = await _service.DeleteAsync(id, cancellationToken);
                if (delete.IsSuccess)
                {
                    return await Result<bool>.SuccessAsync(delete.Data, ResponseStatus.DeletedSuccess, true);
                }
                return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
            }
            catch (Exception)
            {
                throw new ArgumentNullException(nameof(id));
            }
        }

    }
}
