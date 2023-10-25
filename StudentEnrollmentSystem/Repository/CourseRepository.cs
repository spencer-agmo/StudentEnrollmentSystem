
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Models.Auth;
using StudentEnrollmentSystem.UOW;

namespace StudentEnrollmentSystem.Repository
{
    public class CourseRepository : ICourse
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseRepository(DataContext context, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        public async Task<dynamic> GetCourses()
        {
            var courses = _unitOfWork.GetRepository<Course>();
            var result = courses.Get();
            return new ServiceResponse<IEnumerable<Course>>(result, "Get courses successful.");
        }

        public async Task<dynamic> GetCourseById(int courseId)
        {
            var courses = _unitOfWork.GetRepository<Course>();
            var result = courses.GetByID(courseId);
            if (result == null)
            {
                return new ServiceResponse<string>( "Course not found.");
            }
            return new ServiceResponse<Course>(result, "Get course successful.");
        }

        public async Task<dynamic> CreateCourse(CourseDTO course)
        {
            var data = _mapper.Map<CourseDTO, Course>(course);
            var courses = _unitOfWork.GetRepository<Course>();
            courses.Insert(data);
            _unitOfWork.Commit();
            return new ServiceResponse<string>("Course successfully added.");
        }

        public async Task<dynamic> UpdateCourse(int id, CourseDTO course)
        {
            var data = _mapper.Map<CourseDTO, Course>(course);
            var courses = _unitOfWork.GetRepository<Course>();
            var existingCourse = courses.GetByID(id);

            if (existingCourse == null)
            {
                return new ServiceResponse<string>($"Course with ID {id} not found.");
            }

            // Update the properties of the existing course
            existingCourse.CourseName = data.CourseName;
            existingCourse.Room = data.Room;
            existingCourse.AvailableSlot = data.AvailableSlot;
            _unitOfWork.Commit();
            return new ServiceResponse<string>("Course successfully updated.");
        }

        public async Task<dynamic> DeleteCourse(int courseId)
        {
            var courses = _unitOfWork.GetRepository<Course>();
            var existingCourse = courses.GetByID(courseId);

            if (existingCourse == null)
            {
                return new ServiceResponse<string>($"Course with ID {courseId} not found.");
            }

            courses.Delete(courseId);
            _unitOfWork.Commit();
            return new ServiceResponse<string>("Course successfully deleted.");
        }




    }
}
