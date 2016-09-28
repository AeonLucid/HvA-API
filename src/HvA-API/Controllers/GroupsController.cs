using System;
using System.Threading.Tasks;
using HvA.API.NetStandard1;
using HvA.API.NetStandard1.Data;
using HvA_API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HvA_API.Controllers
{
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private readonly HvAClient _client;
        private readonly IMemoryCache _memoryCache;

        public GroupsController(HvAClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("search/{filter}")]
        public async Task<Schedule[]> Search(string filter)
        {
            var cacheKey = $"GroupsController-Search-{filter}";

            return await _memoryCache.GetValueAsync(cacheKey, async () => await _client.GetSchedulesAsync(filter), TimeSpan.FromMinutes(1));
        }
    }
}
