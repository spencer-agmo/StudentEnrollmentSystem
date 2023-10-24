
using Microsoft.EntityFrameworkCore;
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

        public async Task<ICollection<Course>> GetCourses()
        {
            return await _context.Courses.OrderBy(course => course.CourseId).ToListAsync();
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync(course => course.CourseId == courseId);
        }

        public async Task CreateCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            var existingCourse = await _context.Courses.FindAsync(course.CourseId);

            if (existingCourse == null)
            {
                throw new ArgumentException($"Course with ID {course.CourseId} not found.");
            }

            // Update the properties of the existing course
            existingCourse.CourseName = course.CourseName;
            existingCourse.Room = course.Room;
            existingCourse.AvailableSlot = course.AvailableSlot;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
            {
                throw new ArgumentException($"Course with ID {courseId} not found.");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }




    }
}
