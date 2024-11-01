using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Dtos.UserDtos.Request;
using E_commerceWebApi.Application.DTOs.UserDtos.Request;
using E_commerceWebApi.Application.DTOs.UserDtos.Response;
using E_commerceWebApi.Application.Services.Image.Interface;
using E_commerceWebApi.Application.Services.User.Interface;
using E_commerceWebApi.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using OnMapper;
using System.IdentityModel.Tokens.Jwt;

namespace E_commerceWebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IImageService _imageService;
        private readonly OnMapping _mapper;
        private readonly IEmailSender _emailSender;
        public AuthController(IUserService service, 
            IImageService imageService, 
            OnMapping mapper,
            IEmailSender emailSender)
        {
            _imageService = imageService;
            _service = service;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<UserResponseDto>>> Get()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value;

                var result = await _service.GetAllAsync();

                return await Result<IEnumerable<UserResponseDto>>.SuccessAsync(result.Data, "Get All users Successfully", true);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("getUser/{userId}")]
        public async Task<Result<UserResponseDto>> Get(string userId)
        {
            var result = await _service.GetByIdAsync(userId);

            return await Result<UserResponseDto>.SuccessAsync(result.Data, "User is found Successfully", true);
        }

        [HttpPost]
        [Route("login")]
        public async Task<Result<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var response = await _service.Login(request);

            if (response.IsSuccess)
            {
                return await Result<LoginResponseDto>.SuccessAsync(response.Data, "Logged Successfully", true);
            }

            return await Result<LoginResponseDto>.FaildAsync(false, response.Message);
        }

        [HttpPost]
        [Route("register")]
        public async Task<Result<UserResponseDto>> Register([FromForm] RegisterRequestDto request)
        {

            if (request.file == null)
            {
                //return BadRequest(new { message = "The file field is required." });
                throw new Exception();
            }

            var imageUrl = await _imageService.UploadImage(request, request.file);

            request.ImageUrl = imageUrl.Data;

            var response = await _service.Register(request);


            if (response.IsSuccess)
            {

                var mappedUser = await _mapper.Map<UserResponseDto, UserRequestDto>(response.Data);
                var code = await _service.GenerateUserEmailConfirmationTokenAsync(mappedUser.Data);
                var callbackUrl = Url.Action("ConfirmEmail", "Auth", new
                {
                    userid = mappedUser.Data.Id,
                    code
                }, protocol: HttpContext.Request.Scheme);


                //Method for sending email to 
                await _emailSender.SendEmailAsync(request.Email, "Confirm Email",
                    $"Please confirm your email by clicking here : <a href='{callbackUrl}'>Link</a>");
                return await Result<UserResponseDto>.SuccessAsync(response.Data, "Account is Created Successfully", true);
            }

            return await Result<UserResponseDto>.FaildAsync(false, "Account is already in use");
        }

        [HttpPost]
        [Route("roleAssign")]
        public async Task<Result<bool>> UserRole([FromBody] UserRoleRequestDto request)
        {
            var response = await _service.AddUserToRole(request);
            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, "Role is Added Successfully", true);
            }

            return await Result<bool>.FaildAsync(false, "Role not added");
        }


        [HttpPost]
        [Route("emailConfirmation")]
        public async Task<Result<string>> ConfirmEmail(string code, string userId)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _service.FindUserByIdAsync(userId);

                if (userEmail is null)
                {
                    return await Result<string>.FaildAsync(false, "Not Found");
                }

                var mappedRequest = await _mapper.Map<UserResponseDto, UserRequestDto>(userEmail.Data);

                var result = await _service.ConfirmUserEmailAsync(mappedRequest.Data, code);
                if (result.IsSuccess)
                {
                    return await Result<string>.SuccessAsync("EmailConfirmed Successfully", true);
                }
            }
            return await Result<string>.FaildAsync(false, "ModelStateError");
        }

        [HttpPost]
        public async Task<Result<bool>> ForgetPassword([FromQuery] ForgetPasswordDto request)
        {
            var response = await _service.ForgetPassword(request);

            if(response.IsSuccess)
            {

                return await Result<bool>.SuccessAsync(response.Data, response.Message, true);
            }

            return await Result<bool>.FaildAsync(false, response.Message);
        }


        [HttpPost]
        public async Task<IActionResult> RessetPassword([FromQuery] RessetPasswordDto dto)
        {
            //if (ModelState.IsValid)
            //{
            //    var auth = await _service.RessetPassword(dto);
            //    if (!auth.IsSucceeded)
            //        return BadRequest(new ApiResponse(auth.Status, auth.Message));
            //    return Ok(auth.Message);
            //}
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize]
        public async Task<Result<string>> EditProfile(EditProfileDto dto)
        {
            var response = await _service.EditProfile(dto);
            if (!response.IsSuccess)
            {
                return await Result<string>.SuccessAsync(response.Message, true);
            }

            return await Result<string>.FaildAsync(false, response.Message);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {
            var response = await _service.RefreshTokenAsync(request);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("updateUser/{userId}")]
        public async Task<Result<UserResponseDto>> Put(string userId, UpdateUserRequestDto user)
        {
            var response = await _service.UpdateAsync(userId, user);

            if (response.IsSuccess)
            {
                return await Result<UserResponseDto>.SuccessAsync(response.Data, "Account is Updated Successfully", true);
            }


            return await Result<UserResponseDto>.FaildAsync(false, "Account is not iupdated");
        }

        [HttpDelete("deleteUser/{userId}")]
        public async Task<Result<bool>> Delete(string userId)
        {
            var response = await _service.DeleteAsync(userId);

            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, response.Message, true);
            }

            return await Result<bool>.FaildAsync(false, "User is not deleted");
        }

        [HttpGet("activateUser/{userId}")]
        public async Task<Result<bool>>Activate(string userId)
        {
            var user = await _service.ActivateUser(userId);

            return await Result<bool>.SuccessAsync(true, ResponseStatus.ActivatedSuccess, true);
        }

        [HttpGet("deactivateUser/{userId}")]
        public async Task<Result<bool>> DeActivate(string userId)
        {
            var user = await _service.DeactivateUser(userId);

            return await Result<bool>.SuccessAsync(true, ResponseStatus.DeActivatedSuccess, true);
        }
    }
}
