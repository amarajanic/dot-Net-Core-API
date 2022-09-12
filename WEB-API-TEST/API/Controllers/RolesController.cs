using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class RolesController : ControllerBase
    {
        IRoleService _service;

        public RolesController(IRoleService service, IMapper mapper)
        {
            _service = service;
            
        }

        [HttpGet]
        public async Task<List<RoleDisplay>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<RoleDisplay>> Get(int id)
        {
            try
            {
                return await _service.GetById(id);
            }
            catch (Exception)
            {
                return NotFound("Role not found!");
            }

        }
    }
}
