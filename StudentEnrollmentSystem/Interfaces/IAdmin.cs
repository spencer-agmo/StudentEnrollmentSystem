using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Models.Auth;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface IAdmin
    {
        Task<dynamic> GetAdminUsersAsync();
        Task<dynamic> SignUp([FromBody] SignUpDTO model);
        Task<dynamic> GetUserProfileById(string id);
        Task<dynamic> UpdateUserProfile(string id, ProfileDTO model);
        Task<dynamic> DeleteUser(string id);
        Task<dynamic> GetAllEnrollmentsAsync();
        Task<dynamic> ApproveOrRejectEnrollment(int enrollmentId, int status);

    }
}
