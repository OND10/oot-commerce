
using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.UserDtos.Request;
using E_commerceWebApi.Application.DTOs.UserDtos.Request;
using E_commerceWebApi.Application.DTOs.UserDtos.Response;
using E_commerceWebApi.Application.Services.User.Interface;
using E_commerceWebApi.Domain.Entities;
using E_commerceWebApi.Domain.Interfaces;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnMapper;
using System.Security.Claims;

namespace E_commerceWebApi.Application.Services.User.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenRepository _tokenRepository;
        private readonly OnMapping _mapper;
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitofWork _unitofWork;
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public UserService(
            IUserRepository repository, 
            ITokenRepository tokenRepository,
            OnMapping mapper, 
            IOptions<JwtOptions> jwtOptions,
            IUnitofWork unitofWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
            _unitofWork = unitofWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<bool>> AddUserToRole(UserRoleRequestDto request)
        {
            var result = await _repository.AssignRoleToUser(request.Email, request.roleName);

            if (result is true)
            {
                return await Result<bool>.SuccessAsync(result, "Role is Created Successfully", true);
            }

            return await Result<bool>.FaildAsync(false, "Role is Created Successfully");
        }
        public async Task<Result<IdentityResult>> ConfirmUserEmailAsync(UserRequestDto user, string token)
        {

            var mappedModel = await _mapper.Map<UserRequestDto, ApplicationUser>(user);

            //await _

            var result = await _repository.ConfirmUserEmailAsync(mappedModel.Data, token);

            return await Result<IdentityResult>.SuccessAsync(result, "Email Confirmed Successfully", true);
        }
        public async Task<Result<bool>> DeleteAsync(string userId)
        {
            var result = await _repository.DeleteAsync(userId);

            return await Result<bool>.SuccessAsync(result, "Deleted Successfully", true);
        }
        public async Task<Result<UserResponseDto>> FindUserByIdAsync(string userId)
        {
            var result = await _repository.FindUserByIdAsync(userId);

            var mappedResponse = await _mapper.Map<ApplicationUser, UserResponseDto>(result);

            return await Result<UserResponseDto>.SuccessAsync(mappedResponse.Data, "User is Found Successfully", true);
        }
        public async Task<Result<UserResponseDto>> FindUserByUsernameAsync(string username)
        {
            var result = await _repository.FindUserByNameAsync(username);

            var mappedResponse = await _mapper.Map<ApplicationUser, UserResponseDto>(result);

            return await Result<UserResponseDto>.SuccessAsync(mappedResponse.Data, "User is Found Successfully", true);
        }
        public async Task<Result<string>> GenerateUserEmailConfirmationTokenAsync(UserRequestDto user)
        {

            var mappedModel = await _mapper.Map<UserRequestDto, ApplicationUser>(user);

            var result = await _repository.GenerateUserEmailConfirmationTokenAsync(mappedModel.Data);

            return await Result<string>.SuccessAsync(result, "Email token is Generate Successfully", true);
        }
        public async Task<Result<IEnumerable<UserResponseDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            var mappedResponse = await _mapper.MapCollection<ApplicationUser, UserResponseDto>(result);

            return await Result<IEnumerable<UserResponseDto>>.SuccessAsync(mappedResponse.Data, "Get All Users Successfully", true);
        }
        public async Task<Result<UserResponseDto>> GetByIdAsync(string Id)
        {
            var result = await _repository.GetByIdAsync(Id);

            var mappedModel = new UserResponseDto
            {
                Id = result.Id,
                Email = result.Email,
                Name = result.Name,
                PhoneNumber = result.PhoneNumber,
                NoneHashedPassword = result.NoneHashedPassword,
                ImageUrl = result.ImageUrl
            };

            return await Result<UserResponseDto>.SuccessAsync(mappedModel, "User is Found Successfully", true);
        }
        public async Task<Result<IEnumerable<string>>> GetUserRolesAsync(UserRequestDto user)
        {

            var mappedModel = await _mapper.Map<UserRequestDto, ApplicationUser>(user);

            return await Result<IEnumerable<string>>.SuccessAsync(await _repository.GetUserRolesAsync(mappedModel.Data), "Ueer Roles are found", true);
        }
        public async Task<Result<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var checkUsernameResult = await _repository.FindUserByNameAsync(request.UserName);

            if (checkUsernameResult is not null)
            {
                if(checkUsernameResult.IsActive == true)
                {

                    //Checking password
                    var checkPasswordResult = await _repository.CheckUserPasswordAsync(checkUsernameResult, request.Password);

                    if (checkPasswordResult)
                    {
                        var roles = await _repository.GetUserRolesAsync(checkUsernameResult);

                        //Creating JWT
                        var tokens = _tokenRepository.CreateJWTTokenAsync(checkUsernameResult, roles.ToList());
                        var refreshToken = _tokenRepository.CreateRefreshToken();

                        checkUsernameResult.RefreshToken = refreshToken.Result;
                        checkUsernameResult.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiration);

                        //Mapping ApplicationUser to the LoginResponseDTO
                        var response = new LoginResponseDto
                        {
                            User = new UserResponseDto
                            {
                                Id = checkUsernameResult.Id,
                                Email = checkUsernameResult.Email,
                                Name = checkUsernameResult.Name,
                                PhoneNumber = checkUsernameResult.PhoneNumber,
                                ImageUrl = checkUsernameResult.ImageUrl,
                                NoneHashedPassword = checkUsernameResult.NoneHashedPassword,
                            },
                            Token = tokens.Result,
                            RefreshToken = refreshToken.Result,
                            Roles = roles.ToList()
                        };
                        return await Result<LoginResponseDto>.SuccessAsync(response, "Logged Successfully", true);
                    }
                }
                else
                {
                    return await Result<LoginResponseDto>.FaildAsync(false, "User is not activated");
                }
                return await Result<LoginResponseDto>.FaildAsync(false, "Email or Password are incorrect");
            }
            return await Result<LoginResponseDto>.FaildAsync(false, "Email or Password are incorrect");
        }
        public async Task<Result<UserResponseDto>> Register(RegisterRequestDto request)
        {
            var mappedModel = await _mapper.Map<RegisterRequestDto, ApplicationUser>(request);
            mappedModel.Data.NoneHashedPassword = request.Password;
            mappedModel.Data.Name = request.UserName;

            var result = await _repository.CreateUserAsync(mappedModel.Data, request.Password);

            if (result.Succeeded)
            {
                var checkuserEmailResult = await _repository.FindUserByEmailAsync(request.Email);

                ///
                /// Here I should use mapping but still I am in debugging mode.
                ///
                var userMappedResult = await _mapper.Map<ApplicationUser, UserResponseDto>(checkuserEmailResult);

                return await Result<UserResponseDto>.SuccessAsync(userMappedResult.Data, "Registered Successfully", true);
            }

            return await Result<UserResponseDto>.FaildAsync(false, $"{result.Errors}");
        }
        public async Task<Result<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            if (request == null)
            {
                return await Result<RefreshTokenResponseDto>.FaildAsync(false, "Invalid client request");
            }

            var principal = await _tokenRepository.GetPrincipalFromExpiredToken(request.AccessToken);
            var userEmail = principal.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return await Result<RefreshTokenResponseDto>.FaildAsync(false, "Invalid client request");
            }

            var user = await _repository.FindUserByEmailAsync(userEmail);
            if (user == null)
            {
                return await Result<RefreshTokenResponseDto>.FaildAsync(false, "User not found");
            }

            if (user.RefreshToken == request.RefreshToken)
            {
                return await Result<RefreshTokenResponseDto>.FaildAsync(false, "Invalid refresh token");
            }

            // Check if refresh token has expired
            if (user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return await Result<RefreshTokenResponseDto>.FaildAsync(false, "Refresh token has expired");
            }
            var roles = await _repository.GetUserRolesAsync(user);
            var newAccessToken = _tokenRepository.CreateJWTTokenAsync(user, roles);
            var newRefreshToken = _tokenRepository.CreateRefreshToken();

            user.RefreshToken = newRefreshToken.Result;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiration);
            await _repository.UpdateAsync(user);

            var response = new RefreshTokenResponseDto(newAccessToken.Result, newRefreshToken.Result);

            return await Result<RefreshTokenResponseDto>.SuccessAsync(response, "Token refreshed successfully", true);
        }
        public async Task<Result<UserResponseDto>> UpdateAsync(string userId, UpdateUserRequestDto request)
        {
            var model = new ApplicationUser
            {
                Id = userId,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _repository.UpdateAsync(model);

            var mappedModel = new UserResponseDto
            {
                Id = result.Id,
                Name = result.Name,
                PhoneNumber = result.PhoneNumber,
                NoneHashedPassword = result.NoneHashedPassword,
                Email = result.Email,
                ImageUrl = result.ImageUrl
            };

            return await Result<UserResponseDto>.SuccessAsync(mappedModel, "User updated Successfully", true);
        }

        public async Task<Result<bool>> ActivateUser(string userId)
        {
            var user = await _repository.GetByIdAsync(userId);

            if(user is not null)
            {
                user.IsActive = true;

                await _unitofWork.SaveEntityChangesAsync();
                return await Result<bool>.SuccessAsync(true, ResponseStatus.ActivatedSuccess, true);
            }
            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
        }

        public async Task<Result<bool>> DeactivateUser(string userId)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (user is not null)
            {
                user.IsActive = false;
                await _unitofWork.SaveEntityChangesAsync();
                return await Result<bool>.SuccessAsync(true, ResponseStatus.DeActivatedSuccess, true);
            }
            return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
        }

        public async Task<Result<string>> EditProfile(EditProfileDto request)
        {
            var user = await GetUser(); 

            if (user is not null)
            {
                user.Email = request.Email;
                user.Name = request.Name;
                user.PhoneNumber = request.PhoneNumber;

                await _repository.UpdateAsync(user);

                return await Result<string>.SuccessAsync("User profile is updated Successfully", true);
            }

            return await Result<string>.FaildAsync(false, "User is not updated");
        }

        private async Task<ApplicationUser> GetUser()
        {
            ClaimsPrincipal User = _httpContextAccessor.HttpContext.User;
            
            var userid = await _repository.GetUserIdByClaim(User);
            
            var users = await _repository.GetByIdAsync(userid);
            
            return await Task.FromResult(users);
        }

        public async Task<Result<bool>> ForgetPassword(ForgetPasswordDto request)
        {
            var result = await _repository.ForegetPassword(request.Email);

            if(result is not true)
            {
                return await Result<bool>.FaildAsync(false, ResponseStatus.Faild);
            }

            return await Result<bool>.SuccessAsync(true, ResponseStatus.Success, true);
        }
    }
}
