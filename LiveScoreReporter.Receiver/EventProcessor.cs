using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories;
using LiveScoreReporter.Receiver.Application.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace LiveScoreReporter.Receiver
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IGenericRepository<Score> _scoreRepository;

        public EventProcessor(IGenericRepository<Event> eventRepository, IGenericRepository<Score> scoreRepository)
        {
            _eventRepository = eventRepository;
            _scoreRepository = scoreRepository;
        }

        public async Task ProcessEventAsync(string message)
        {
            try
            {
                var eventData = JsonConvert.DeserializeObject<EventFromQueue>(message);

                var typeOfEvent = ConvertToEnum(eventData.Type);

                var existingEvent = await _eventRepository.SelectAsync(e =>
                    e.GameId == eventData.FixtureId &&
                    e.Time == eventData.Time.Elapsed &&
                    e.Type == typeOfEvent &&
                    e.Details == eventData.Detail &&
                    e.TeamId == eventData.Team.Id &&
                    e.PlayerId == eventData.Player.Id);

                if (existingEvent != null)
                {
                    return;
                }

                var newEvent = new Event
                {
                    GameId = eventData.FixtureId,
                    Time = eventData.Time.Elapsed,
                    Type = typeOfEvent,
                    Details = eventData.Detail,
                    TeamId = eventData.Team.Id,
                    PlayerId = eventData.Player.Id,
                    AssistPlayerId = eventData.Assist.Id
                };

                _eventRepository.Add(newEvent);
                await _eventRepository.SaveAsync();


                if (typeOfEvent == EventType.Goal)
                {
                    await UpdateScoreAsync(newEvent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateScoreAsync(Event eventData)
        {
            var score = await _scoreRepository.SelectAsync(s => s.GameId == eventData.GameId);

            if (eventData.Team.Id == score.Game.HomeTeamId)
            {
                score.Home += 1;
            }
            else if (eventData.Team.Id == score.Game.AwayTeamId)
            {
                score.Away += 1;
            }

            _scoreRepository.Update(score);
            await _scoreRepository.SaveAsync();
        }

        public EventType ConvertToEnum(string value)
        {
            return value.ToLower() switch
            {
                "goal" => EventType.Goal,
                "card" => EventType.Card,
                "subst" => EventType.Subst,
                "var" => EventType.Var,
                _ => throw new Exception("unexpected event")
            };
        }
    }
}
