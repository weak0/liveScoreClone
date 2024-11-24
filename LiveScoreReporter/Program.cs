using LiveScoreReporter.Shared.Hub;
using MediatR;
using System.Reflection;
using FluentValidation;
using LiveScoreReporter.Infrastucture;
using LiveScoreReporter.Seeder;
using MediatR.Extensions.FluentValidation.AspNetCore;

namespace LiveScoreReporter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MatchDb");
      
            builder.Services.AddInfrastructure(connectionString!);
            builder.Services.AddServices();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddPolicy();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin() 
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });


            var app = builder.Build();
            
            // SEEED DATABASE
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var seeder = services.GetRequiredService<DbSeeder>();
            await seeder.SeedAsync();

            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseSwagger();
            
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                options.RoutePrefix = string.Empty;
            });
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MatchHub>("/matchHub");
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
