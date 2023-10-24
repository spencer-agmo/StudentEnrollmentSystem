using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface ICourse
    {
        ICollection<Course> GetCourses();
     void UpdateCourse(Course course);
        void CreateCourse(Course course);
        void DeleteCourse(int courseId);
        Course GetCourseById(int courseId);
    }
}
