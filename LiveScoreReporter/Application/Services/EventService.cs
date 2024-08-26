using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;

namespace LiveScoreReporter.Application.Services
{
    public class EventService : IEventService
    {

        private readonly IEventRepository _eventRepository;
        private readonly ISerializerService _serializerService;

        public EventService(IEventRepository eventRepository, ISerializerService serializerService)
        {
            _eventRepository = eventRepository;
            _serializerService = serializerService;
        }

        public async Task<List<Event>> GetGameEventsWithDetailsAsync(int gameId)
        {
            return await _eventRepository.GetAllEventsForGame(gameId);
        }

        public List<EventWithDetailsDto> MapEventsToDto(List<Event> eventsWithDetails)
        {
            return eventsWithDetails.Select(e => new EventWithDetailsDto
            {
                TeamId = e.TeamId,
                TeamName = e.Team.Name,
                Type = e.Type,
                Details = e.Details,
                Comments = e.Comments,
                Time = e.Time,
                PlayerName = e.Player.Name,
                AssistPlayerName = e.AssistPlayer?.Name,
            }).ToList();
        }

        public string SerializeEventsToJson(List<EventWithDetailsDto> eventWithDetailsDtos)
        {
            return _serializerService.SerializeObjectToJson(eventWithDetailsDtos);
        }
    }
}
