using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Models.Auth;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface IAdmin
    {
        Task<IActionResult> GetAdminUsersAsync();
        Task<IActionResult> SignUp([FromBody] SignUpDTO model);
        Task<IActionResult> GetUserProfileById(string id);
        Task<IActionResult> UpdateUserProfile(string id, ProfileDTO model);
        Task<IActionResult> DeleteUser(string id);
        Task<IActionResult> GetAllEnrollmentsAsync();
        Task<IActionResult> ApproveOrRejectEnrollment(int enrollmentId, int status);

    }
}
