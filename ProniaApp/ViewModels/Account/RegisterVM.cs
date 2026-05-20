using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace ProniaApp.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "First name is required.")]
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters long.")]
        [MaxLength(10, ErrorMessage = "First name cannot exceed 10 characters.")]   
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(3, ErrorMessage = "Last name must be at least 3 characters long.")]
        [MaxLength(12, ErrorMessage = "Last name cannot exceed 12 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
        [MaxLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [MinLength(5, ErrorMessage = "Email must be at least 5 characters long.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
