using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Abstract;
using ServiceLayer.Models;

namespace StudentApp.Controllers
{
    [Route("api/students")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class StudentController : Controller
    {
        private readonly IStudentManager _manager;

        public StudentController(IStudentManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get list of students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetStudents([FromQuery]StudentRequest request)
        {
            var result = _manager.GetAllStudents(request);
            return Ok(result);
        }

        /// <summary>
        /// Get student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var result = _manager.GetStudent(id);
            return Ok(result);
        }

        /// <summary>
        /// Create student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateStudent([FromBody]StudentCreateRequest student)
        {
            var result = _manager.CreateStudent(student);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Edit student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateStudent([FromBody]StudentUpdateRequest student)
        {
            var result = _manager.UpdateStudent(student);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Delete student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var result = _manager.DeleteStudent(id);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}