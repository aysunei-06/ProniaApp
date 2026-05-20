namespace ProniaApp.Areas.Admin.ViewModels.Update
{
    public class UpdateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Discount { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
