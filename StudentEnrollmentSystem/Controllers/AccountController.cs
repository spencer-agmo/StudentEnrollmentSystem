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

            var result = await _accountRepository.SignUp(model);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
        {
            var result = await _accountRepository.SignIn(userLoginResource);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var result = await _accountRepository.ChangePassword(model);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var result = await _accountRepository.GetUserProfileById(id);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("profile/{id}")]

        public async Task<IActionResult> UpdateUserProfile(string id, ProfileDTO model)
        {
            var result = await _accountRepository.UpdateUserProfile(id, model);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

    }


}
