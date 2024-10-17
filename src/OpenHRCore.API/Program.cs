using FluentValidation.AspNetCore;
using OpenHRCore.Application;
using OpenHRCore.Infrastructure;
using Serilog;
using Serilog.Events;

namespace OpenHRCore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .CreateLogger();

            Log.Information("Starting up OpenHRCore.API");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddOpenHRCoreWorkForceDbContext(builder.Configuration);
            builder.Services.AddOpenHRCoreWorkForceInfrastructure();
            builder.Services.AddOpenHRCoreWorkForceApplication();

            var app = builder.Build();

            Log.Information("Application build completed");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            try
            {
                var urls = app.Configuration["ASPNETCORE_URLS"]?.Split(',').ToArray();

                if (urls != null)
                {
                    foreach (var url in urls)
                    {
                        Log.Information("OpenHRCore.API is running on {Address}", url);
                    }
                }
                Log.Information("Running OpenHRCore.API");
                app.Run();


            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
