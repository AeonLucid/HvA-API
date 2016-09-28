using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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
