using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.BrandDtos.Request;
using E_commerceWebApi.Application.Dtos.BrandDtos.Response;
using E_commerceWebApi.Application.Dtos.CategoryDtos.Request;
using E_commerceWebApi.Application.Dtos.CategoryDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Application.Services.Brands.Interface
{
    public interface IBrandService
    {
        Task<Result<BrandResponseDto>> CreateAsync(BrandRequestDto entity, CancellationToken cancellationToken);
        Task<Result<IEnumerable<BrandResponseDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<BrandResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<BrandResponseDto>> UpdateAsync(UpdateBrandRequestDto entity, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(int Id, CancellationToken cancellationToken);
    }
}
