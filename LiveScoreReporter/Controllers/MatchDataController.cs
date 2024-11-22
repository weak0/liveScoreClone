using LiveScoreReporter.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
