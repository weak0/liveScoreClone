using System.Security.Claims;
using LiveScoreReporter.Application.Commands;
using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator, IAuthService authService)
    {
        _mediator = mediator;
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<bool>> Register([FromBody] RegisterForm registerForm)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(new { StatusCode = 400, Message = "Validation failed", Errors = errors });
        }

        RegisterCommand command = new(registerForm);
        var result = await _mediator.Send(command);

        return result ? Ok(true) : BadRequest(new { StatusCode = 500, Message = "Registration failed" });
    }

    [HttpPost("Login")]
    public async Task<ActionResult<bool>> Login([FromBody] LoginForm loginForm)
    {
        LoginCommand command = new(loginForm);

        var result = await _mediator.Send(command);

        if (result)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(_authService.GetClaimsIdentity(loginForm.Login)));

            return Ok(result);
        }

        return BadRequest();
    }

    [HttpGet("IsAuthenticated")]
    [Authorize]
    public ActionResult IsAuthenticated()
    {
        if (User.Identity.IsAuthenticated) return Ok();

        throw new ApplicationException("Unauthorized");
    }

    [HttpPost("Logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }
}