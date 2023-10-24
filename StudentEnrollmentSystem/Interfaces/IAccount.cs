using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Models.Auth;
using System;

namespace StudentEnrollmentSystem.Interfaces
{
    public interface IAccount
    {
        Task<dynamic> SignUp([FromBody] SignUpDTO model);
        Task<dynamic> SignIn(UserLoginResource userLoginResource);
        Task<dynamic> ChangePassword([FromBody] ChangePasswordModel model);
        Task<dynamic> GetUserProfileById(string id);
        Task<dynamic> UpdateUserProfile(string id, ProfileDTO model);

    }
}
