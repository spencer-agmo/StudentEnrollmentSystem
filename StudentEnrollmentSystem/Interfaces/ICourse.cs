using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface ICourse
    {
        Task<dynamic> GetCourses();
        Task<dynamic> UpdateCourse(int id,CourseDTO course);
        Task<dynamic> CreateCourse(CourseDTO course);
        Task<dynamic> DeleteCourse(int courseId);
        Task<dynamic> GetCourseById(int courseId);
    }
}
