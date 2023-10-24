using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Models.Auth;
using StudentEnrollmentSystem.Settings;

namespace StudentEnrollmentSystem.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdmin _adminRepository;
        public AdminController(IAdmin adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAdminUsersAsync()
        {
            return await _adminRepository.GetAdminUsersAsync();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPost]

        public async Task<IActionResult> SignUp([FromBody] SignUpDTO model)
        {
            return await _adminRepository.SignUp(model);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            return await _adminRepository.GetUserProfileById(id);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(string id, ProfileDTO model)
        {
            return await _adminRepository.UpdateUserProfile(id, model);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return await _adminRepository.DeleteUser(id);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet("enrollments")]
        public async Task<IActionResult> GetAllEnrollmentsAsync()
        {
            return await _adminRepository.GetAllEnrollmentsAsync();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPut("enrollments/approval/{enrollmentId}")]
        public async Task<IActionResult> ApproveOrRejectEnrollment(int enrollmentId, int status)
        {
            return await _adminRepository.ApproveOrRejectEnrollment(enrollmentId, status);
        }
    }
}
enum EnrollmentStatus : ushort
{
    Reject = 0,
    Pending = 1,
    Approve = 2

}