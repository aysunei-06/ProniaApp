using System.ComponentModel.DataAnnotations;

namespace ProniaApp.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username or email is required")]

        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Remember me is required")]
        public bool IsPersistent { get; set; }
    }
}
