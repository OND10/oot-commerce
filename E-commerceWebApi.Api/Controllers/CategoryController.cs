using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.CategoryDtos.Request;
using E_commerceWebApi.Application.Dtos.CategoryDtos.Response;
using E_commerceWebApi.Application.Services.Categories.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnMapper;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly OnMapping _mapper;

        public CategoryController(ICategoryService service, OnMapping mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<CategoryResponseDto>>> Get(CancellationToken cancellationToken)
        {

            var result = await _service.GetAllAsync(cancellationToken);

            return await Result<IEnumerable<CategoryResponseDto>>.SuccessAsync(result.Data, ResponseStatus.GetAllSuccess, true);
        }

        [HttpGet("{id}")]
        public async Task<Result<CategoryResponseDto>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);

            return await Result<CategoryResponseDto>.SuccessAsync(result.Data, ResponseStatus.GetSuccess, true);
        }

        [Authorize]
        [HttpPost]
        public async Task<Result<CategoryResponseDto>> Post([FromBody] CategoryRequestDto model, CancellationToken cancellationToken)
        {

            var result = await _service.CreateAsync(model, cancellationToken);

            if (result.IsSuccess)
            {
                return await Result<CategoryResponseDto>.SuccessAsync(result.Data, ResponseStatus.CreateSuccess, true);
            }

            return await Result<CategoryResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<Result<CategoryResponseDto>> Put([FromRoute] int id, [FromBody] CategoryRequestDto model, CancellationToken cancellationToken)
        {
            try
            {
                var mappedRequestModel = await _mapper.Map<CategoryRequestDto, UpdateCategoryRequestDto>(model);

                mappedRequestModel.Data.Id = id;

                var updateResult = await _service.UpdateAsync(mappedRequestModel.Data, cancellationToken);

                return await Result<CategoryResponseDto>.SuccessAsync(updateResult.Data, ResponseStatus.UpdateSuccess, true);
            }
            catch (Exception)
            {
                return await Result<CategoryResponseDto>.FaildAsync(false, ResponseStatus.Faild);
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
