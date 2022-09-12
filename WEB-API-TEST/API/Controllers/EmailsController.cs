using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        IEmailService _service;

        public EmailsController(IEmailService service, IMapper mapper)
        {
            _service = service;
        }


        [HttpPost]
        public IActionResult SendEmail(string recipient,string subject, string body)
        {
            _service.SendEmail(recipient, body, subject);

            return Ok();
        }
    }
}
