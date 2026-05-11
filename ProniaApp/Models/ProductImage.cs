namespace ProniaApp.Models
{
    public class ProductImage:Base
    {
        public string ImageUrl { get; set; }
        public bool? IsPrimary { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
