
using LiveScoreReporter.Application.Services;
using LiveScoreReporter.EFCore.Infrastructure;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories;
using LiveScoreReporter.MockApiAssets.Services;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreReporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MatchDb");
            // Add services to the container.
            builder.Services.AddDbContext<LiveScoreReporterDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IMatchService, MatchService>();
            builder.Services.AddScoped<IMockedDataService, MockedDataService>();
            builder.Services.AddScoped<IGenericRepository<Game>, GameRepository>();
            builder.Services.AddScoped<IGenericRepository<Team>, TeamRepository>();
            builder.Services.AddScoped<IGenericRepository<Event>, EventRepository>();
            builder.Services.AddScoped<IGenericRepository<Player>, PlayerRepository>();
            builder.Services.AddScoped<IGenericRepository<Score>, ScoreRepository>();
            builder.Services.AddScoped<IGenericRepository<League>, LeagueRepository>();

            builder.Services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
