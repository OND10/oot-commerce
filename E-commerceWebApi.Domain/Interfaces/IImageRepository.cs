using Microsoft.AspNetCore.Http;

namespace E_commerceWebApi.Domain.Shared
{
    public interface IImageRepository
    {
        Task<string> Upload(object model , IFormFile file);
    }
}
