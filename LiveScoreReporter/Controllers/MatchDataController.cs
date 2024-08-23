using LiveScoreReporter.Application.Services;
using LiveScoreReporter.MockApiAssets.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LiveScoreReporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchDataController : ControllerBase
    {
        private readonly IMatchService _matchService;
        
        public MatchDataController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFixture(int fixtureId)
        {
            var data = await _matchService.GetMatchDetailsAsync(fixtureId);
           
            var result = await _matchService.AddDataToDb(data);

            if (result)
                return Ok(data);

            return BadRequest();
        }
    }
}
