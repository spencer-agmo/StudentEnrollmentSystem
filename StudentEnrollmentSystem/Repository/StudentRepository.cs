using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Models.Auth;

namespace StudentEnrollmentSystem.Repository
{
    public class StudentRepository : IStudent
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _DataContext;
        private readonly IMapper _mapper;
        public StudentRepository(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, DataContext DataContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _DataContext = DataContext;
        }
        public async Task<dynamic> PostEnrollment(EnrollmentDTO enrollment)
        {
            var enroll = _mapper.Map<Enrollment>(enrollment);
            _DataContext.Enrollments.Add(enroll);

            var existingCourse = _DataContext.Courses.Find(enrollment.CourseId);

            if (existingCourse == null)
            {
                throw new ArgumentException($"Course with ID {enrollment.CourseId} not found.");
            }

            // Update the properties of the existing course

            existingCourse.AvailableSlot -= 1;
            await _DataContext.SaveChangesAsync();

            return new ServiceResponse<string>("Ok","Enrollment submmitted");
        }
        public async Task<dynamic> DeleteEnrollment(int enrollmentId)
        {
            var enrollment = await _DataContext.Enrollments.FindAsync(enrollmentId);

            if (enrollment == null)
            {
                return new ServiceResponse<string>("Enrollment not found");
            }

            var existingCourse = _DataContext.Courses.Find(enrollment.CourseId);
            existingCourse.AvailableSlot += 1;
            _DataContext.Enrollments.Remove(enrollment);
            await _DataContext.SaveChangesAsync();

            return new ServiceResponse<string>("Ok","Enrollment deleted successfully");
        }
        public dynamic GetEnrollmentsByUserId(string userId)
        {
            var enrollments = _DataContext.Enrollments
                .Where(e => e.UserId == userId)
                 .AsNoTrackingWithIdentityResolution()
                .ToList();

            if (!enrollments.Any())
            {
                return new ServiceResponse<string>("No enrollments found for the user");
            }

            var enrollmentModels = enrollments.Select(enrollment => new
            {
                EnrollmentId = enrollment.EnrollmentId,
                Status = enrollment.Status,
                CourseId = enrollment.CourseId,
                UserId = enrollment.UserId

            }).ToList();

            var courseInfo = enrollments.Select(enrollment => new
            {
                CourseInfo = _DataContext.Courses
            .Where(course => course.CourseId == enrollment.CourseId)
            .Select(course => new
            {
                CourseName = course.CourseName,
                Room = course.Room,
                AvailableSlot = course.AvailableSlot
            })
            .FirstOrDefault()
            }).ToList();

            var result = enrollmentModels.Zip(courseInfo, (enrollment, course) => new
            {
                EnrollmentId = enrollment.EnrollmentId,
                Status = enrollment.Status,
                CourseId = enrollment.CourseId,
                UserId = enrollment.UserId,
                CourseInfo = course.CourseInfo
            }).ToList();

            return new ServiceResponse<Object>(result,"Enrollments get by userid successful.");
        }
    }
}
