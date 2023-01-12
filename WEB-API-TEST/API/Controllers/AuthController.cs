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
        public async Task<ActionResult<string>> Register(UserRegister request)
        {
            try
            {
                var response = await _service.Register(request);

                return response;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error while registrating new user!");
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(UserLogin request)
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
        [HttpPost("Refresh")]
        public async Task<ActionResult<LoginResponse>> Refresh([FromBody]string refresh)
        {
            try
            {
                var response = await _service.Refresh(refresh);

                return response;
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err.Message);
            }
        }
    }
}
