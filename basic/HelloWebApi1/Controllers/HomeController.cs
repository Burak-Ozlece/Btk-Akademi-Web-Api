using HelloWebApi1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWebApi1.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            var result = new ResponseModel()
            {
                HttpStatus=200,
                Message = "Hello Word"
            };
            return Ok(result);
        }
    }
}
