using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Models.Auth;
using StudentEnrollmentSystem.UOW;
using System;
using System.Collections.Generic;

namespace StudentEnrollmentSystem.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(ICourse courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;  
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {

            var result = await _courseRepository.GetCourses();
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet("{id}", Name = "GetCourseById")]
        public async Task<IActionResult> GetCourseAsync(int id)
        {
            var result = await _courseRepository.GetCourseById(id);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDTO course)
        {
            var result = await _courseRepository.CreateCourse(course);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseDTO course)
        {
            var result = await _courseRepository.UpdateCourse( id, course);
            if (result?.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseRepository.DeleteCourse(id);
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
