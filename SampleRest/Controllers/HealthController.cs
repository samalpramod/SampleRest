using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleRest.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        // GET: api/<RestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Ok" };
        }

    }
}
