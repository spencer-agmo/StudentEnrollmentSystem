using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models.Auth;
using StudentEnrollmentSystem.Settings;

namespace StudentEnrollmentSystem.Repository
{
    public class AdminRepository : Controller,IAdmin
    {


        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        public AdminRepository(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, DataContext DataContext, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _dataContext = DataContext;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<IActionResult> GetAdminUsersAsync()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync("admin");

            return Ok(adminUsers);
        }
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<SignUpDTO, User>(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var role = _userManager.Users.SingleOrDefault(u => u.Email == model.Email);

                    var resultRole = await _userManager.AddToRoleAsync(role, "admin");

                    if (resultRole.Succeeded)
                    {
                        return Ok("Admin created successfully");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("Admin not found");
            }

            var userProfile = _mapper.Map<ProfileDTO>(user);

            return Ok(userProfile);
        }
        public async Task<IActionResult> UpdateUserProfile(string id, ProfileDTO model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            _mapper.Map(model, user);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("Admin profile updated successfully");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _userManager.DeleteAsync(user);


            return Ok("Admin deleted.");
        }
        public async Task<IActionResult> GetAllEnrollmentsAsync()
        {
            var enrollments = _dataContext.Enrollments.AsNoTrackingWithIdentityResolution().ToList();

            var enrollmentModels = enrollments.Select(enrollment => new
            {
                EnrollmentId = enrollment.EnrollmentId,
                Status = enrollment.Status,
                CourseId = enrollment.CourseId,
                UserId = enrollment.UserId

            }).ToList();
            var courseInfo = enrollments.Select(enrollment => new
            {
                CourseInfo = _dataContext.Courses
           .Where(course => course.CourseId == enrollment.CourseId)
           .Select(course => new
           {
               CourseName = course.CourseName,
               Room = course.Room,
               AvailableSlot = course.AvailableSlot
           })
           .FirstOrDefault()
            }).ToList();

            // Retrieve user information for each enrollment
            var userInfo = enrollments.Select(enrollment => new
            {
                UserInfo = enrollment.UserId,
            }).ToList();
            List<object> userProfiles = new List<object>();
            for (var i = 0; i < userInfo.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(userInfo[i].UserInfo);

                if (user != null)
                {
                    var userProfile = new
                    {
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    userProfiles.Add(userProfile);
                }
            }

            var result = enrollmentModels.Zip(courseInfo, (enrollment, course) => new
            {
                EnrollmentId = enrollment.EnrollmentId,
                Status = enrollment.Status,
                CourseId = enrollment.CourseId,
                UserId = enrollment.UserId,
                CourseInfo = course.CourseInfo
            }).Zip(userProfiles, (enrollment, user) => new
            {
                EnrollmentId = enrollment.EnrollmentId,
                Status = enrollment.Status,
                CourseId = enrollment.CourseId,
                UserId = enrollment.UserId,
                CourseInfo = enrollment.CourseInfo,
                StudentInfo = user
            }).ToList();
            return Ok(result);
        }
        public async Task<IActionResult> ApproveOrRejectEnrollment(int enrollmentId, int status)
        {
            var enroll = _dataContext.Enrollments.Find(enrollmentId);

            if (enroll == null)
            {
                return NotFound("User not found");
            }
            if (status == 0)
            {
                enroll.Status = (int)EnrollmentStatus.Reject;
            }
            else if (status == 2)
            {
                enroll.Status = (int)EnrollmentStatus.Approve;
            }



            await _dataContext.SaveChangesAsync();
            return Ok("Enrollment updated successfully");
        }
    }
    enum EnrollmentStatus : ushort
    {
        Reject = 0,
        Pending = 1,
        Approve = 2

    }
}

