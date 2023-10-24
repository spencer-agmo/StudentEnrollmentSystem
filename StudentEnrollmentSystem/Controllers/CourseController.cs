using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Models;
using System;
using System.Collections.Generic;

namespace StudentEnrollmentSystem.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseRepository;

        public CourseController(ICourse courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _courseRepository.GetCourses();
            return Ok(courses);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet("{id}", Name = "GetCourseById")]
        public IActionResult GetCourse(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPost]
        public IActionResult CreateCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }

            _courseRepository.CreateCourse(course);
            return CreatedAtRoute("GetCourseById", new { id = course.CourseId }, course);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] Course course)
        {
            if (course == null || course.CourseId != id)
            {
                return BadRequest();
            }

            var existingCourse = _courseRepository.GetCourseById(id);
            if (existingCourse == null)
            {
                return NotFound();
            }

            _courseRepository.UpdateCourse(course);
            return Ok(true);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }

            _courseRepository.DeleteCourse(id);
            return Ok(true);
        }
    }
}
