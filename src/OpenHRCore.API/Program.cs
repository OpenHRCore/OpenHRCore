using Microsoft.AspNetCore.Identity;
using OpenHRCore.API.ServicesConfiguration;
using OpenHRCore.Infrastructure.Data;
using Serilog;
using Serilog.Events;

namespace OpenHRCore.API
{
    /// <summary>
    /// The main entry point for the OpenHRCore API application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method that starts the application.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            ConfigureLogger();

            try
            {
                Log.Information("Starting up OpenHRCore.API");

                var builder = CreateAndConfigureWebApplicationBuilder(args);
                var app = BuildAndConfigureWebApplication(builder);

                RunApplication(app);
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

        /// <summary>
        /// Configures the Serilog logger.
        /// </summary>
        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .CreateLogger();
        }

        /// <summary>
        /// Creates and configures the WebApplicationBuilder.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>A configured WebApplicationBuilder.</returns>
        private static WebApplicationBuilder CreateAndConfigureWebApplicationBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

            builder.Host.UseSerilog();
            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.Services.AddValidatorsService();
            builder.Services.AddSwaggerGenService();
            builder.Services.AddLocalizationService();
            builder.Services.AddOpenHRCoreServices(builder.Configuration, logger);
            builder.Services.AddScoped<CountryData.Standard.CountryHelper>();


            return builder;
        }


        /// <summary>
        /// Builds and configures the WebApplication.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder to use.</param>
        /// <returns>A configured WebApplication.</returns>
        private static WebApplication BuildAndConfigureWebApplication(WebApplicationBuilder builder)
        {
            var app = builder.Build();

            Log.Information("Application build completed");

            ConfigureMiddleware(app);

            return app;
        }

        /// <summary>
        /// Configures the middleware for the application.
        /// </summary>
        /// <param name="app">The WebApplication to configure.</param>
        private static void ConfigureMiddleware(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.MapControllers();

        }

        /// <summary>
        /// Runs the application and logs the URLs it's running on.
        /// </summary>
        /// <param name="app">The WebApplication to run.</param>
        private static void RunApplication(WebApplication app)
        {
            LogApplicationUrls(app);

            Log.Information("Running OpenHRCore.API");
            app.Run();
        }

        /// <summary>
        /// Logs the URLs the application is running on.
        /// </summary>
        /// <param name="app">The WebApplication to get the URLs from.</param>
        private static void LogApplicationUrls(WebApplication app)
        {
            var urls = app.Configuration["ASPNETCORE_URLS"]?.Split(',');

            if (urls != null)
            {
                foreach (var url in urls)
                {
                    Log.Information("OpenHRCore.API is running on {Address}", url);
                }
            }
        }
    }
}
