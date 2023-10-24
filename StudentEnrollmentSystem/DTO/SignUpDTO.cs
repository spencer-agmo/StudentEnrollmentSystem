using FluentValidation;
using StudentEnrollmentSystem.Models.Auth;

namespace StudentEnrollmentSystem.DTO
{
    public class SignUpDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
   
}
