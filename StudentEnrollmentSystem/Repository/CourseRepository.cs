
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models;

namespace StudentEnrollmentSystem.Repository
{
    public class CourseRepository : ICourse
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Course> GetCourses()
        {
            return _context.Courses.OrderBy(course => course.CourseId).ToList();
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.FirstOrDefault(course => course.CourseId == courseId);
        }
        public void CreateCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            var existingCourse = _context.Courses.Find(course.CourseId);

            if (existingCourse == null)
            {
                throw new ArgumentException($"Course with ID {course.CourseId} not found.");
            }

            // Update the properties of the existing course
            existingCourse.CourseName = course.CourseName;
            existingCourse.Room = course.Room;
            existingCourse.AvailableSlot = course.AvailableSlot;

            _context.SaveChanges();
        }

        public void DeleteCourse(int courseId)
        {
            var course = _context.Courses.Find(courseId);

            if (course == null)
            {
                throw new ArgumentException($"Course with ID {courseId} not found.");
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();
        }

      

        
    }
}
