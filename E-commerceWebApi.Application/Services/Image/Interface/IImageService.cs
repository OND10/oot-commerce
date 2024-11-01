
using E_commerceWebApi.Application.Common.Handler;
using Microsoft.AspNetCore.Http;

namespace E_commerceWebApi.Application.Services.Image.Interface
{
    public interface IImageService
    {
        Task<Result<string>> UploadImage(object model, IFormFile file);
    }
}
