using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Models.Auth;

namespace StudentEnrollmentSystem.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudent _studentRepository;
        public StudentController(IStudent studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "student")]
        [HttpPost("enrollments")]
        public async Task<ActionResult<Enrollment>> PostEnrollment(EnrollmentDTO enrollment)
        {
           return await _studentRepository.PostEnrollment(enrollment);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "student")]
        [HttpDelete("enrollments/{enrollmentId}")]
        public async Task<IActionResult> DeleteEnrollment(int enrollmentId)
        {
           return await _studentRepository.DeleteEnrollment(enrollmentId);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "student")]
        [HttpGet("enrollments/{userId}")]
        public IActionResult GetEnrollmentsByUserId(string userId)
        {
            return _studentRepository.GetEnrollmentsByUserId(userId);   
        }
    }
}
