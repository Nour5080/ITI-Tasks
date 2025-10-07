using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }

        // Optional ReturnUrl for redirect after login
        public string? ReturnUrl { get; set; }
    }
}