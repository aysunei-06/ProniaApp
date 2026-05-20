using System.ComponentModel.DataAnnotations.Schema;

namespace ProniaApp.Areas.Admin.ViewModels.Create
{
    public class CreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public decimal Discount { get; set; }
        public IFormFile Photo { get; set; } 
    }
}
