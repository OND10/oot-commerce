
namespace E_commerceWebApi.Application.DTOs.UserDtos.Response
{
    public class LoginResponseDto
    {
        private UserResponseDto user { get; set; }
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; } 


        public UserResponseDto User
        {
            get
            {
                return new UserResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    ImageUrl = user.ImageUrl,
                    
                };
            }
            set
            {
                user = value;
            }
        }
    }
}
