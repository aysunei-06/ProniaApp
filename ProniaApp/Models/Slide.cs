using System.ComponentModel.DataAnnotations.Schema;

namespace ProniaApp.Models
{
    public class Slide : Base
    {
        public string Name { get; set; }    
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public decimal Discount { get; set; }

    }
}
