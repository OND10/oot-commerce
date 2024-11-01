﻿
using E_commerceWebApi.Application.Common.Handler;
using E_commerceWebApi.Application.Shared.Validations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace E_commerceWebApi.Application.DTOs.UserDtos.Request
{
    public class RegisterRequestDto : IEmailValidation, IPhoneNumberValidation, IPasswordValidation
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Incorrect Password !")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password Not Matching !")]
        [Display(Name = "Repeat Pawword ")]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string ImageUrl {  get; set; } = "test";
        public IFormFile file { get; set; }

        public async Task<Result<string>> Emailvalidate()
        {
            string validEmail = IBaseValidation.EmailRegx;
            if (string.IsNullOrEmpty(Email))
            {
                return await Result<string>.FaildAsync(false, "Please Enter an email");
            }
            if (!Regex.IsMatch(Email, validEmail))
            {
                return await Result<string>.FaildAsync(false, "Please Enter a correct Email to be considered");
            }
            
            return null;
        }

        public async Task<Result<string>> Passwordvalidate()
        {
            if (Password.Length is <= 6)
            {
                return await Result<string>.FaildAsync(false, "Password must be more than 6 characters");
            }

            return null;
        }

        public async Task<Result<string>> PhoneNumbervalidate()
        {
            string validPhoneNumber = IBaseValidation.PhoneNumberRegx;
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                return await Result<string>.FaildAsync(false, "Please Enter a PhoneNumber");
            }
            if (!Regex.IsMatch(PhoneNumber, validPhoneNumber))
            {
                return await Result<string>.FaildAsync(false, "Please Enter a correct PhoneNumber to be considered");
            }
            return null;
        }
    }
}
