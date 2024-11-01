
using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.UserDtos.Request;
using E_commerceWebApi.Application.DTOs.UserDtos.Request;
using E_commerceWebApi.Application.DTOs.UserDtos.Response;
using E_commerceWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_commerceWebApi.Application.Services.User.Interface
{
    public interface IUserService
    {
        Task<Result<LoginResponseDto>> Login(LoginRequestDto request);
        Task<Result<UserResponseDto>> Register(RegisterRequestDto request);
        Task<Result<bool>> AddUserToRole(UserRoleRequestDto request);
        Task<Result<string>> GenerateUserEmailConfirmationTokenAsync(UserRequestDto user);
        Task<Result<UserResponseDto>> FindUserByIdAsync(string userId);
        Task<Result<IdentityResult>> ConfirmUserEmailAsync(UserRequestDto user, string token);
        Task<Result<UserResponseDto>> FindUserByUsernameAsync(string username);
        Task<Result<IEnumerable<string>>> GetUserRolesAsync(UserRequestDto user);
        Task<Result<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<Result<IEnumerable<UserResponseDto>>> GetAllAsync();
        Task<Result<UserResponseDto>> GetByIdAsync(string Id);
        Task<Result<UserResponseDto>> UpdateAsync(string userId, UpdateUserRequestDto request);
        Task<Result<bool>> DeleteAsync(string userId);
        Task<Result<bool>> ActivateUser(string userId);
        Task<Result<bool>> DeactivateUser(string userId);
        Task<Result<string>> EditProfile(EditProfileDto request);
        Task<Result<bool>> ForgetPassword(ForgetPasswordDto request);

    }
}
