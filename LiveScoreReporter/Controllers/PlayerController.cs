using LiveScoreReporter.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers;

[ApiController]
public class PlayerController(IPlayerService playerService) : ControllerBase
{
    
}