using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models.Auth;
using StudentEnrollmentSystem.Repository;
using StudentEnrollmentSystem.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentEnrollmentSystem.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccount _accountRepository;
        public AccountController(IAccount accountRepository)
        {
         
            _accountRepository = accountRepository;
          
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO model)
        {

           return await  _accountRepository.SignUp(model);    
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
        {
            return await _accountRepository.SignIn(userLoginResource);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            return await _accountRepository.ChangePassword(model);
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            return await _accountRepository.GetUserProfileById (id);   
        }

        [HttpPut("profile/{id}")]

        public async Task<IActionResult> UpdateUserProfile(string id, ProfileDTO model)
        {
            return await _accountRepository.UpdateUserProfile(id, model);
        }

    }


}
