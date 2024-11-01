using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.ProductDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductDtos.Response;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Request;
using E_commerceWebApi.Application.Dtos.ProductSizesDtos.Response;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.ProductsSizes.Interface
{
    public interface IProductSizesService
    {
        Task<Result<ProductSizesResponseDto>> CreateAsync(ProductSizesRequestDto entity);
        Task<Result<ProductSizesResponseDto>> UpdateAsync(ProductSizesRequestDto entity, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(int Id);
        Task<Result<IEnumerable<ProductSizesResponseDto>>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
    }
}
