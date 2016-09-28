using System.Threading.Tasks;
using HvA.API.NetStandard1;
using HvA.API.NetStandard1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HvA_API.Controllers
{
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private readonly HvAClient _client;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(HvAClient client, ILogger<GroupsController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        [Route("search/{filter}")]
        [ResponseCache(Duration = 60)]
        public async Task<Schedule[]> Search(string filter)
        {
            var result = await _client.GetSchedulesAsync(filter);

            _logger.LogWarning("Hi test");

            return result;
        }
    }
}
