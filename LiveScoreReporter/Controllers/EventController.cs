using LiveScoreReporter.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("/events/{gameId}")]
        public async Task<IActionResult> GetAllEventsForParticularGameAsync([FromRoute] int gameId)
        {
            var eventsWithPlayersAndTeams = await _eventService.GetGameEventsWithDetailsAsync(gameId);

            var eventsWithDetailsDtos = _eventService.MapEventsToDto(eventsWithPlayersAndTeams);

            var dtosSerializedToJson = _eventService.SerializeEventsToJson(eventsWithDetailsDtos);

            return Ok(dtosSerializedToJson);
        }
    }
}
