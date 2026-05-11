namespace ProniaApp.Models
{
    public class Product: Base
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int CategoryId { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public Category Category { get; set; }
    }
}
