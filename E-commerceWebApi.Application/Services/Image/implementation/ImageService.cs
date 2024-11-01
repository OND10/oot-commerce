
using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Services.Image.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace E_commerceWebApi.Application.Services.Image.implementation
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;
        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> UploadImage(object model, IFormFile file)
        {
            
            var result = await _repository.Upload(model, file);

            return await Result<string>.SuccessAsync(result, ResponseStatus.UploadedSuccess, true);
        }
    }
}
