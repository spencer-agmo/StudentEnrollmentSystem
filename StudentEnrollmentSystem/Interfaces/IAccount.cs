using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Models.Auth;
using System;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface IAccount
    {
          Task<IActionResult> SignUp([FromBody] SignUpDTO model);
          Task<IActionResult> SignIn(UserLoginResource userLoginResource);
          Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model);
          Task<IActionResult> GetUserProfileById(string id);

          Task<IActionResult> UpdateUserProfile(string id, ProfileDTO model);
    
    }
}
