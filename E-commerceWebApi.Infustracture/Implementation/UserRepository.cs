using E_commerceWebApi.Domain.Common.Exceptions;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Infustracture.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace E_commerceWebApi.Infustracture.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }


        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);

            return result;
        }

        public async Task<bool> AssignRoleToUser(string email, string roleName)
        {
            var result = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (result != null)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await AddUserToRoleAsync(result, roleName);

                return true;
            }
            return false;
        }

        public async Task<bool> CheckUserPasswordAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> ConfirmUserEmailAsync(ApplicationUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            var user = await FindUserByIdAsync(userId);

            if (user is not null)
            {
                _context.ApplicationUsers.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }

            throw new IdNullException(nameof(userId));
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            return result;
        }

        public Task<ApplicationUser> FindUserByNameAsync(string userName)
        {
            var result = _userManager.FindByNameAsync(userName);
            return result;
        }

        public async Task<string> GenerateUserEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return result;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var result = await _context.ApplicationUsers.ToListAsync();
            return result;
        }

        public async Task<ApplicationUser> GetByIdAsync(string Id)
        {
            var result = await _context.ApplicationUsers.Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (result is not null)
            {
                return result;
            }
            else
            {
                throw new IdNullException($"{result} is null");
            }
        }

        public IdentityOptions Options { get; set; }
        public Task<string> GetUserIdByClaim(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException("principal");
            }

            return Task.FromResult<string>(claimsPrincipal.FindFirstValue(Options.ClaimsIdentity.UserIdClaimType));
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            var existingUser = await _context.ApplicationUsers.FirstOrDefaultAsync(c => c.Id == user.Id);

            if (existingUser == null)
            {
                throw new ModelNullException($"{user.Id}", "Model is null");
            }

            existingUser.Name = user.Name;
            existingUser.UserName = user.Name;
            existingUser.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> ForegetPassword(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if(user is not null)
            {
                var token = await GenerateUserEmailConfirmationTokenAsync(user);

                var newOtp = GenerateOtpCode(6);

                await _emailSender.SendEmailAsync(email, "ForgetPassword", $" IS Your OTP  Code:\a{newOtp}\a.Do Not Share it With anyone");

                return true;
            }

            return false;
        }



        //public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        //{
        //    IUserPasswordStore<ApplicationUser> passwordStore = GetPasswordStore();
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException("user");
        //    }

        //    if (await VerifyPasswordAsync(passwordStore, user, currentPassword) != 0)
        //    {
        //        IdentityResult identityResult = await UpdatePasswordHash(passwordStore, user, newPassword);
        //        if (!identityResult.Succeeded)
        //        {
        //            return identityResult;
        //        }

        //        return await UpdateUserAsync(user);
        //    }

        //    Logger.LogWarning(LoggerEventIds.ChangePasswordFailed, "Change password failed for user.");
        //    return IdentityResult.Failed(ErrorDescriber.PasswordMismatch());
        //}

        // Function to create a new opt passing it length to generate based on it  
        private string GenerateOtpCode(int length)
        {
            var random = new Random();

            string otp = "";

            for (int r = 0; r < length; r++)
            {
                otp += random.Next(0, 10).ToString();

            }
            return otp;
        }
        //private IUserPasswordStore<ApplicationUser> GetPasswordStore()
        //{
        //    return (Store as IUserPasswordStore<ApplicationUser>) ?? throw new NotSupportedException(Resources.StoreNotIUserPasswordStore);
        //}
    }
}
