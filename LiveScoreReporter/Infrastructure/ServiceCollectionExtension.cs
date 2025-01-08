using System.Reflection;
using LiveScoreReporter.Application.Services;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure;
using LiveScoreReporter.EFCore.Infrastructure.Repositories;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using LiveScoreReporter.Seeder;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreReporter.Infrastucture;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMatchService, SeederService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IScoreRepository, ScoreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILeagueRepository, LeagueRepository>();
        services.AddScoped<ILineupRepository, LineupRepository>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ISerializerService, SerializerService>();
        services.AddScoped<DbSeeder>();
        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
   
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSignalR();
        services.AddSwaggerGen();
        services.AddHttpClient();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        return services;
    }

    public static IServiceCollection AddPolicy(this IServiceCollection services)
    {
        
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.Name = "LSR.Authentication";
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            options.Cookie.IsEssential = true;
            options.SlidingExpiration = true;
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            };
        });

        return services;
    }
    
}