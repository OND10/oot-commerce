using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.SubCategoryDtos.Request;
using E_commerceWebApi.Application.Dtos.SubCategoryDtos.Response;
using E_commerceWebApi.Application.Services.SubCategories.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using OnMapper;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _service;
        private readonly OnMapping _mapper;
        public SubCategoryController(ISubCategoryService service, OnMapping mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<SubCategoryResponseDto>>> Get()
        {
            var response = await _service.GetAllAsync();

            if (response.IsSuccess)
            {
                return await Result<IEnumerable<SubCategoryResponseDto>>.SuccessAsync(response.Data, ResponseStatus.GetAllSuccess, true);
            }

            return await Result<IEnumerable<SubCategoryResponseDto>>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPost]
        public async Task<Result<SubCategoryResponseDto>> CreateAsync(SubCategoryRequestDto entity)
        {

            var result = await _service.CreateAsync(entity);

            if (result.IsSuccess)
            {
                return await Result<SubCategoryResponseDto>.SuccessAsync(result.Data, ResponseStatus.CreateSuccess, true);
            }

            return await Result<SubCategoryResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpDelete("{Id}")]
        public async Task<Result<bool>> DeleteAsync(int Id)
        {
            var response = await _service.DeleteAsync(Id);

            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, ResponseStatus.DeletedSuccess, true);
            }

            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
        }


        [HttpGet("{categoryId}")]
        public async Task<Result<SubCategoryResponseDto>> GetByCategoryIdAsync(int categoryId)
        {
            var response = await _service.GetByCategoryIdAsync(categoryId);

            if (response.IsSuccess)
            {
                return await Result<SubCategoryResponseDto>.SuccessAsync(response.Data, ResponseStatus.GetSuccess, true);
            }

            return await Result<SubCategoryResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpGet]
        public async Task<Result<SubCategoryResponseDto>> GetByIdAsync(int id)
        {
            var response = await _service.GetByIdAsync(id);

            if (response.IsSuccess)
            {
                return await Result<SubCategoryResponseDto>.SuccessAsync(response.Data, ResponseStatus.GetSuccess, true);
            }

            return await Result<SubCategoryResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<Result<SubCategoryResponseDto>> UpdateAsync([FromRoute] int id, [FromBody] SubCategoryRequestDto entity)
        {


            var mappedRequestModel = await _mapper.Map<SubCategoryRequestDto, UpdateSubCategoryRequestDto>(entity);

            mappedRequestModel.Data.Id = id;

            var response = await _service.UpdateAsync(mappedRequestModel.Data);

            if (response.IsSuccess)
            {
                return await Result<SubCategoryResponseDto>.SuccessAsync(response.Data, ResponseStatus.UpdateSuccess, true);
            }

            return await Result<SubCategoryResponseDto>.FaildAsync(true, ResponseStatus.Faild);
        }


    }
}
