using E_commerceWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> CreateJWTTokenAsync(ApplicationUser entity, IEnumerable<string> roles);
        Task<string> CreateRefreshToken();
        Task<ClaimsPrincipal>GetPrincipalFromExpiredToken(string token);
    }
}
