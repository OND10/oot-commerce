﻿using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ImageRepository(IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor)
        {
            _webHost = webHost;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Upload(object model, IFormFile file)
        {
            var orgModel = model.GetType();
            string modelName = orgModel.FullName;
            string abbreModelName = modelName.Substring(0, modelName.IndexOf('R'));


            ImageValidation.ValidationFileUpload(file);

            if (file != null)
            {
                // Uploading the image to the sepcified folder
                var folderPath = System.IO.Path.Combine(_webHost.ContentRootPath, "Images", $"{abbreModelName}");
                var localPath = System.IO.Path.Combine(folderPath, file.FileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                using var stream = new FileStream(localPath, FileMode.Create);
                await file.CopyToAsync(stream);

                //Store filename and extenstion to the DB
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{abbreModelName}/{file.FileName}";


                return await Task.FromResult<string>(urlPath);
            }

            return await Task.FromResult<string>("");
        }
    }
}
