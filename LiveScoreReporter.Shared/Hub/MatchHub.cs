using Microsoft.AspNetCore.SignalR;

namespace LiveScoreReporter.Shared.Hub
{
    public class MatchHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendEventUpdate(string gameId, string eventData)
        {
            await Clients.All.SendAsync("ReceiveEventUpdate", gameId, eventData);
        }
    }
}
