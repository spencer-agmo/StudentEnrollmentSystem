using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models.Auth;
using StudentEnrollmentSystem.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentEnrollmentSystem.Repository
{
    public class AccountRepository : Controller, IAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        IValidator<SignUpDTO> _validator;
        public AccountRepository(
     IMapper mapper,
     UserManager<User> userManager,
     RoleManager<Role> roleManager,
     IOptionsSnapshot<JwtSettings> jwtSettings,
     IValidator<SignUpDTO> validator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _validator = validator;
        }
        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

           public async Task<IActionResult> SignUp([FromBody] SignUpDTO model)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<SignUpDTO,User>(model) ;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var role = _userManager.Users.SingleOrDefault(u => u.Email == model.Email);

                    var resultRole = await _userManager.AddToRoleAsync(role, "student");

                    if (resultRole.Succeeded)
                    {
                        return Ok("Student registered successfully");
                    }
                }



                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }
        public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userLoginResource.Email);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);
            var data = await _userManager.FindByEmailAsync(userLoginResource.Email);
            if (userSigninResult)
            {

                var roles = await _userManager.GetRolesAsync(user);
                return Ok(GenerateJwt(user, roles));

            }

            return BadRequest("Email or password incorrect.");
        }
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    return Ok("Password changed successfully");
                }

                foreach (var error in changePasswordResult.Errors)
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
                return NotFound("User not found");
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
                return Ok("User profile updated successfully");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
    }
}
