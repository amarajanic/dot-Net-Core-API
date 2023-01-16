using DataAccess.DbModel;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<UserDisplay>> Get([FromQuery]int skip = 0,[FromQuery]int take = 100)
        {
            return await _service.GetAll(skip, take);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDisplay>> GetById(int id)
        {
            try
            {
                return await _service.GetUserById(id);
            }
            catch (Exception)
            {
                return NotFound("User not found!");
            }

        }
        [HttpGet("Role/{id}")]
        public async Task<ActionResult<List<UserDisplay>>> GetByRoleId(int id)
        {
            try
            {
                return await _service.GetUsersByRoleId(id);
            }
            catch (Exception)
            {
                return NotFound("User with that role not found!");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post(UserInsert user)
        {
            try
            {
                var response = await _service.InsertUser(user);
                return Ok(response);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
            } 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UserInsert request, int id)
        {
            try 
            { 
                var response = await _service.UpdateUser(request, id);
                return Ok(response);
            }
            catch (Exception)
            {
                return NotFound("User not found!");
            }
        }

        [HttpPatch("Change-Password")]
        public async Task<IActionResult> Patch(UserChangePassword request)
        {
            try
            {
                if (request.NewPassword != request.RepeatPassword)
                    return BadRequest("Passwords don't match!");

                var email = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                var response = await _service.ChangePassword(request, email);
                return Ok(response);
            }
            catch (Exception)
            {
                return NotFound("There was an error while changing your password!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _service.DeleteUser(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return NotFound("User not found!");
            }

        }
    }
}
