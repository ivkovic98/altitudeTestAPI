using Altitude.Bussiness.Interfaces;
using Altitude.Bussiness.Models.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AltitudeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<ActionResult<StatisticsResponseModel>> Get()
        {
            var stats = await _statisticsService.GetStatisticsAsync();
            return Ok(stats);
        }
    }
}