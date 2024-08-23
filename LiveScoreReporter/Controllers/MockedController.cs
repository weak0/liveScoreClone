using LiveScoreReporter.Application.Services;
using LiveScoreReporter.MockApiAssets.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockedController : ControllerBase
    {
        private readonly IMockedDataService _mockedDataService;
        private readonly IMatchService _matchService;

        public MockedController(IMockedDataService mockedDataService, IMatchService matchService)
        {
            _mockedDataService = mockedDataService;
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMockFixture()
        {
            var data = await _mockedDataService.GetMockedMatchDetailsAsync();

            var result = await _matchService.AddDataToDb(data);

            if (result)
                return Ok(data);

            return BadRequest();
        }
    }
}
