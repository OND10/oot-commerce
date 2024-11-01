using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace E_commerceWebApi.Infustracture.Implementation
{
    public class ProductImagesRepository : IProductImagesRepository
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductImagesRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHost)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _webHost = webHost;
        }



        public async Task<IEnumerable<ProductImages>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _context.ProductImages.ToListAsync();

            if (result.Count > 0)
            {
                return result;
            }
            return Enumerable.Empty<ProductImages>();
        }

        public async Task<IEnumerable<ProductImages>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var result = await _context.ProductImages.Where(p => p.ProductId == productId).ToListAsync();

            if (result.Count > 0)
            {
                return result;
            }
            return Enumerable.Empty<ProductImages>();

        }

        public async Task<ProductImages> UploadAsync(IEnumerable<IFormFile> files, int productId, string rootPath)
        {
            ProductImages productImages = null;

            foreach (var file in files)
            {
                var model = new ProductImages
                {
                    ImageExtension = Path.GetExtension(file.FileName).ToLower(),
                    ImageName = file.FileName,
                    CreatedAt = DateTime.Now,
                    ProductId = productId,
                };

                // Ensure the correct folder path: <rootPath>/Images/Products
                var folderPath = Path.Combine(_webHost.WebRootPath, "Images/Products");
                var uniqueFile = Guid.NewGuid().ToString() + "_" + model.ImageName; 
                var localPath = Path.Combine(folderPath, uniqueFile);

                // Ensure the directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Save the file to the local path
                using (var stream = new FileStream(localPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Construct the URL path to be stored in the database
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/Products/{model.ImageName}";

                // Assign the URL to the image model and store it in the database
                model.ImageUrl = urlPath;
                await _context.ProductImages.AddAsync(model);
                productImages = model;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return productImages;
        }


        public async Task<bool> DeleteAsync(int imageId, CancellationToken cancellationToken)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);

            if (productImage == null)
                return false; // Image not found in the database

            // Use the Images folder directly from your application directory
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Products");
            var imagePath = Path.Combine(imagesFolder, Path.GetFileName(productImage.ImageName));

            // Check if the image file exists before attempting deletion
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            // Remove the image entry from the database
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }



        public async Task<ProductImages> UpdateAsync(int imageId, IFormFile file)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image != null)
            {
                // Delete the old image file
                var oldFilePath = Path.Combine(_webHost.ContentRootPath, "Images", "Products", image.ImageName);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                // Save the new image file
                var newFileName = file.FileName;
                var newFilePath = Path.Combine(_webHost.ContentRootPath, "Images", "Products", newFileName);
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Update image details in the database
                image.ImageName = newFileName;
                image.ImageExtension = Path.GetExtension(file.FileName);
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                image.ImageUrl = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/Products/{newFileName}";

                _context.ProductImages.Update(image);
                await _context.SaveChangesAsync();
                return image;
            }
            return null;
        }

    }

}
