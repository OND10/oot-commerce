namespace E_commerceWebApi.Application.Dtos.ProductDtos.Request
{
    public class UpdateProductRequestDto
    {
        public int ProductId { get; set;}
        public string EnglishName { get; set; } = string.Empty;
        public string ArabicName { get; set; } = string.Empty;
        public string EnglishDescription { get; set; } = string.Empty;
        public string ArabicDescription { get; set; } = string.Empty;
        public float Price { get; set; }
        public float DiscountPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public bool IsFeatured { get; set; }
        public int SubcategoryId { get; set; }
    }
}
