using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface ICourse
    {
        Task<ICollection<Course>> GetCourses();
        Task UpdateCourse(Course course);
        Task CreateCourse(Course course);
        Task DeleteCourse(int courseId);
        Task<Course> GetCourseById(int courseId);
    }
}
