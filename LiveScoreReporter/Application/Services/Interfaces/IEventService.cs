using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.EFCore.Infrastructure.Entities;

namespace LiveScoreReporter.Application.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetGameEventsWithDetailsAsync(int gameId);
        List<EventWithDetailsDto> MapEventsToDto(List<Event> eventsWithDetails);
        string SerializeEventsToJson(List<EventWithDetailsDto> eventWithDetailsDtos);
    }
}
