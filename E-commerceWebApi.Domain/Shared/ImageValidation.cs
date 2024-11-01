using E_commerceWebApi.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace E_commerceWebApi.Domain.Shared
{
    public static class ImageValidation
    {
        public static bool ValidationFileUpload(IFormFile file)
        {
            var preferedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!preferedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                throw new ModelNullException("file", "Image Format unsupported");
            }

            if (file.Length > 10485760)
            {
                throw new ModelNullException("file", "File size bigger than required");
            }

            return true;
        }
    }
}
