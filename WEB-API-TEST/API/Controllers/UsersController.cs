using DataAccess.DbModel;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
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
            var response = await _service.InsertUser(user);
            return Ok(response);

           /** try
            {
                var response = await _service.InsertUser(user);
                return Ok(response);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
            **/
            

           
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
