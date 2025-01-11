using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;


namespace LiveScoreReporter.Application.Services
{
    public class EventService : IEventService
    {

        private readonly IEventRepository _eventRepository;
        private readonly ISerializerService _serializerService;
        private readonly ISeederService _seederService;

        public EventService(IEventRepository eventRepository, ISerializerService serializerService, ISeederService seederService)
        {
            _eventRepository = eventRepository;
            _serializerService = serializerService;
            _seederService = seederService;
        }

        public async Task<List<Event>> GetGameEventsWithDetailsAsync(int gameId)
        {
            var gameEvents =  await _eventRepository.GetAllEventsForGame(gameId);
            if (gameEvents.Count != 0) return gameEvents;
            await _seederService.SeedGameEventsAsync(gameId);
            gameEvents = await _eventRepository.GetAllEventsForGame(gameId);
            return gameEvents;
        }

        public List<EventWithDetailsDto> MapEventsToDto(List<Event> eventsWithDetails)
        {
            return eventsWithDetails.Select(e => new EventWithDetailsDto
            {
                GameId = e.GameId,
                TeamId = e.TeamId,
                TeamName = e.Team?.Name,
                Type = e.Type,
                Details = e.Details,
                Comments = e.Comments,
                Time = e.Time,
                PlayerName = e.Player?.Name,
                AssistPlayerName = e.AssistPlayer?.Name,
            }).ToList();
        }

        public string SerializeEventsToJson(List<EventWithDetailsDto> eventWithDetailsDtos)
        {
            return _serializerService.SerializeObjectToJson(eventWithDetailsDtos);
        }
    }
}
