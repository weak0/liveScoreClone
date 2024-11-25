using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers;

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
    public async Task<ActionResult<List<EventWithDetailsDto>>> GetAllEventsForParticularGameAsync([FromRoute] int gameId)
    {
        var eventsWithPlayersAndTeams = await _eventService.GetGameEventsWithDetailsAsync(gameId);

        var eventsWithDetailsDto = _eventService.MapEventsToDto(eventsWithPlayersAndTeams);
            
        return Ok(eventsWithDetailsDto);
    }
}