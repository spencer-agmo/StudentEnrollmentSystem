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
            var result = await _studentRepository.PostEnrollment(enrollment);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "student")]
        [HttpDelete("enrollments/{enrollmentId}")]
        public async Task<IActionResult> DeleteEnrollment(int enrollmentId)
        {

            var result = await _studentRepository.DeleteEnrollment(enrollmentId);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "student")]
        [HttpGet("enrollments/{userId}")]
        public IActionResult GetEnrollmentsByUserId(string userId)
        {

            var result = _studentRepository.GetEnrollmentsByUserId(userId);
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
