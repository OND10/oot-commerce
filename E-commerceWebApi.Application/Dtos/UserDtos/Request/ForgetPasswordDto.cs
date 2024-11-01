using System.ComponentModel.DataAnnotations;

namespace E_commerceWebApi.Application.Dtos.UserDtos.Request
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Please Enter Your Email!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter Your Email correctly!")]
        public string Email { get; set; }
    }
}
