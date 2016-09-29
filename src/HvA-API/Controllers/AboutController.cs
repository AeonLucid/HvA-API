using Microsoft.AspNetCore.Mvc;

namespace HvA_API.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        [HttpGet]
        [Route("ping")]
        public string Ping()
        {
            return "pong";
        }
    }
}
