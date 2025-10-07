using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyFirstProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role must be selected.")]
        public string Role { get; set; }

        // =====================
        // خصائص خاصة بالطالب
        // =====================
        [Range(1, 12, ErrorMessage = "Grade must be between 1 and 12.")]
        public int? Grade { get; set; }

        // =====================
        // خصائص مشتركة / خاصة بالInstructor
        // =====================
        [Range(0, 100000, ErrorMessage = "Salary must be between 0 and 100,000.")]
        public decimal? Salary { get; set; }

        public int? DeptId { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        // Course ID للInstructor
        public int? CrsId { get; set; }

        // رفع صورة
        public IFormFile? ImageFile { get; set; }
    }
}
