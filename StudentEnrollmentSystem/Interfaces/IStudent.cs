using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface IStudent
    {
        Task<dynamic> PostEnrollment(EnrollmentDTO enrollment);
        Task<dynamic> DeleteEnrollment(int enrollmentId);
        dynamic GetEnrollmentsByUserId(string userId);

    }
}
