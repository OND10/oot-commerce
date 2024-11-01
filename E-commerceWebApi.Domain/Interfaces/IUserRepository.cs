using E_commerceWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceWebApi.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<ApplicationUser> FindUserByEmailAsync(string email);
        public Task<bool> CheckUserPasswordAsync(ApplicationUser user, string password);
        public Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        public Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        public Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);
        public Task<string> GenerateUserEmailConfirmationTokenAsync(ApplicationUser user);
        public Task<IdentityResult> ConfirmUserEmailAsync(ApplicationUser user, string token);
        public Task<ApplicationUser> FindUserByIdAsync(string userId);
        public Task<ApplicationUser> FindUserByNameAsync(string userName);
        public Task<bool> AssignRoleToUser(string email, string roleName);
        public Task<ApplicationUser> UpdateAsync(ApplicationUser user);
        public Task<IEnumerable<ApplicationUser>> GetAllAsync();
        public Task<ApplicationUser> GetByIdAsync(string Id);
        public Task<bool> DeleteAsync(string userId);
        public Task<string> GetUserIdByClaim(ClaimsPrincipal claimsPrincipal);
        public Task<bool> ForegetPassword(string email);


    }
}
