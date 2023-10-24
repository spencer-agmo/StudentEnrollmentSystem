using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface IStudent
    {
        Task<ActionResult<Enrollment>> PostEnrollment(EnrollmentDTO enrollment);
        Task<IActionResult> DeleteEnrollment(int enrollmentId);
        IActionResult GetEnrollmentsByUserId(string userId);

    }
}
