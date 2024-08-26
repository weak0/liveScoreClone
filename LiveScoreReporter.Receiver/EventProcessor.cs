﻿using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.Receiver.Application.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;

namespace LiveScoreReporter.Receiver
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IEventRepository _eventRepository;
        private readonly IScoreRepository _scoreRepository;
        private ILogger<EventProcessor> _logger;

        public EventProcessor(IEventRepository eventRepository, IScoreRepository scoreRepository, ILogger<EventProcessor> logger)
        {
            _eventRepository = eventRepository;
            _scoreRepository = scoreRepository;
            _logger = logger;
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
                    AssistPlayerId = typeOfEvent == EventType.Goal ? eventData.Assist.Id : null, //todo wait for response from api support because there may be an error from them.
                };

                _eventRepository.Add(newEvent);
                await _eventRepository.SaveAsync();


                if (typeOfEvent == EventType.Goal)
                {
                     UpdateScoreAsync(newEvent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void UpdateScoreAsync(Event eventData)
        {
            var scoreWithGame =  _scoreRepository
                .Select(s => s.GameId == eventData.GameId, include: query => query.Include(s => s.Game));

            _logger.LogInformation("Before update - Home: {Home}, Away: {Away}", scoreWithGame.Home, scoreWithGame.Away);

            if (eventData.TeamId == scoreWithGame.Game.HomeTeamId)
            {
                _logger.LogInformation("Incrementing Home score for GameId: {GameId}", scoreWithGame.GameId);
                scoreWithGame.Home += 1;
            }
            else if (eventData.TeamId == scoreWithGame.Game.AwayTeamId)
            {
                _logger.LogInformation("Incrementing Away score for GameId: {GameId}", scoreWithGame.GameId);
                scoreWithGame.Away += 1;
            }
            else
            {
                _logger.LogWarning("TeamId {TeamId} did not match HomeTeamId or AwayTeamId for GameId {GameId}", eventData.TeamId, scoreWithGame.GameId);
            }

            _logger.LogInformation("After update - Home: {Home}, Away: {Away}", scoreWithGame.Home, scoreWithGame.Away);


            _scoreRepository.Update(scoreWithGame);
             _scoreRepository.Save();
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