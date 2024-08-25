using LiveScoreReporter.EFCore.Infrastructure;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using LiveScoreReporter.Shared.RabbitMq.Settings;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreReporter.Receiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<RabbitMqSettings>(hostContext.Configuration.GetSection("RabbitMQ"));

                    services.AddDbContext<LiveScoreReporterDbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("MatchDb")));

                    services.AddScoped<IGenericRepository<Event>, EventRepository>();
                    services.AddScoped<IGenericRepository<Score>, ScoreRepository>();
                    services.AddScoped<IEventProcessor, EventProcessor>();

                    services.AddHostedService<ReceiverService>();
                });
    }
}