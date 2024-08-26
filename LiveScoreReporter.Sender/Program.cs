using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using LiveScoreReporter.Sender.Jobs;
using LiveScoreReporter.Sender.RabbitMq;
using LiveScoreReporter.Shared.RabbitMq.Settings;
using RestSharp;

namespace LiveScoreReporter.Sender
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<RestClient>(sp => new RestClient("https://v3.football.api-sports.io/"));

                    services.AddSingleton<IQueueProducer, RabbitMqProducer>();
                    services.Configure<RabbitMqSettings>(hostContext.Configuration.GetSection("RabbitMQ"));

                    services.AddTransient<FetchEventsJob>();

                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionJobFactory();

                        var jobKey = new JobKey("FetchEventsJob");

                        q.AddJob<FetchEventsJob>(opts => opts.WithIdentity(jobKey));

                        q.AddTrigger(opts => opts
                            .ForJob(jobKey)
                            .WithIdentity("FetchEventsJob-trigger")
                            .UsingJobData("fixtureId", 1202998)
                            .WithSimpleSchedule(x => x
                                .WithInterval(TimeSpan.FromMinutes(15))
                                .RepeatForever()));
                    });

                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                })
                .Build();

            await host.RunAsync();
        }
    }
}
