using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace ProniaApp.Models
{
    public class Category:Base
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        public string Name { get; set; }

        public List<Product>? Products { get; set; }
    }
}
