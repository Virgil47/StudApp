using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Abstract;
using ServiceLayer.Models;

namespace StudApp.Controllers
{
    [Route("api/groups")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class GroupController : Controller
    {
        private readonly IGroupManager _manager;
        public GroupController(IGroupManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get list of groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGroups([FromQuery]GroupRequest request)
        {
            var result = _manager.GetAllGroups(request);
            return Ok(result);
        }

        /// <summary>
        /// Get group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public IActionResult GetGroup(int id)
        {
            var result = _manager.GetGroup(id);
            return Ok(result);
        }

        /// <summary>
        /// Create group
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateGroup([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Type group name");
            }
            var result = _manager.CreateGroup(name);
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
        /// Edit group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateGroup([FromBody]GroupUpdateRequest group)
        {
            var result = _manager.UpdateGroup(group);
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
        /// Delete group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup([FromRoute]int id)
        {
            var result = _manager.DeleteGroup(id);
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
        /// Add student to group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpPost("{id}/students/{studentId}")]
        public IActionResult AddStudentToGroup([FromRoute]int studentId, int id)
        {
            var result = _manager.AddStudentToGroup(studentId, id);
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
        /// Delete student from group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}/students/{studentId}")]
        public IActionResult DeleteStudentFromGroup([FromRoute]int studentId, int id)
        {
            var result = _manager.DeleteStudentFromGroup(studentId, id);
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