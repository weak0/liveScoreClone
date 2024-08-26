using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.Shared.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LiveScoreReporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalrController : ControllerBase
    {
        private readonly IHubContext<MatchHub> _hubContext;

        public SignalrController(IHubContext<MatchHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("ProcessEvent")]
        public async Task<IActionResult> ProcessEvent([FromBody] Event newEvent)
        {
            if (newEvent == null)
            {
                return BadRequest();
            }

            string gameId = newEvent.GameId.ToString();
            string eventData = Newtonsoft.Json.JsonConvert.SerializeObject(newEvent);

            await _hubContext.Clients.All.SendAsync("ReceiveEventUpdate", gameId, eventData);

            return Ok();
        }
    }
}
