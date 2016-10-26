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
    public class TimeTableController : Controller
    {
        private readonly HvAClient _client;
        private readonly IMemoryCache _memoryCache;

        public TimeTableController(HvAClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _memoryCache = memoryCache;
        }

        // TODO: Year
        [HttpGet]
        [Route("{studentId}/{weekNumber:int}")]
        public async Task<TimetableItem[]> GetTimeTableWeek(string studentId, int weekNumber)
        {
            var cacheKey = $"TimeTableController-GetTimeTableWeek-{weekNumber}";
            
            return await _memoryCache.GetValueAsync(cacheKey, async () => await _client.GetOtherTimeTableAsync(studentId, weekNumber), TimeSpan.FromMinutes(5));
        }
    }
}
