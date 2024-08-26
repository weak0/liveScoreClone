using LiveScoreReporter.Application.Services;
using LiveScoreReporter.EFCore.Infrastructure;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LiveScoreReporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontendController : ControllerBase
    {
        private readonly IFrontendService _frontendService;

        public FrontendController(IFrontendService frontendService)
        {
            _frontendService = frontendService;
        }

        [HttpGet]
        [Route("/games/all")]
        public async Task<IActionResult> GetAllGamesForLandingPageAsync()
        {
            var gamesWithScoresAndTeams = await _frontendService.GetGamesWithDetailsAsync();

            var gamesWithDetailsDtos =  _frontendService.MapGamesToDto(gamesWithScoresAndTeams);

            var dtosSerializedToJson = _frontendService.SerializeGamesToJson(gamesWithDetailsDtos);

            return Ok(dtosSerializedToJson);
        }
    }
}
