namespace E_commerceWebApi.Application.Dtos.RateDtos.Response
{
    public class ProductRateResponseDto
    {
        public int? Count { get; set; }
        public decimal? Avarage { get; set; }
        public List<UserRateDto>? CustomerRate { get; set; }
    }
}
