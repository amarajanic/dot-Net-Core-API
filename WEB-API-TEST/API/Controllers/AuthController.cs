using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _service;

        public AuthController(IAuthService service, IMapper mapper)
        {
            _service = service;

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDisplay>> Register(UserRegister request)
        {
            try
            {
                var response = await _service.Register(request);

                if (!response)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserLogin request)
        {
            try
            {
                var response = await _service.Login(request);

                return response;
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err.Message);
            }
        }
    }
}
