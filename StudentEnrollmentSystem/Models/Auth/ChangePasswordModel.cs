using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentSystem.Models.Auth
{
    public class ChangePasswordModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Id { get; set; }
    }
}
