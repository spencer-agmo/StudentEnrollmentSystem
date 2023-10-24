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
            var result = await _adminRepository.GetAdminUsersAsync();
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPost]

        public async Task<IActionResult> SignUp([FromBody] SignUpDTO model)
        {
            var result = await _adminRepository.SignUp(model);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var result = await _adminRepository.GetUserProfileById(id);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(string id, ProfileDTO model)
        {
            var result = await _adminRepository.UpdateUserProfile(id, model);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _adminRepository.DeleteUser(id);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet("enrollments")]
        public async Task<IActionResult> GetAllEnrollmentsAsync()
        {
            var result = await _adminRepository.GetAllEnrollmentsAsync();
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPut("enrollments/approval/{enrollmentId}")]
        public async Task<IActionResult> ApproveOrRejectEnrollment(int enrollmentId, int status)
        {
            var result = await _adminRepository.ApproveOrRejectEnrollment(enrollmentId, status);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}