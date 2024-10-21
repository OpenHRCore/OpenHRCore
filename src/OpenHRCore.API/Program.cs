using FluentValidation;
using OpenHRCore.Application;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Infrastructure;
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

            builder.Host.UseSerilog();
            builder.Services.AddControllers();

            AddValidators(builder.Services);
            AddSwagger(builder.Services);
            AddOpenHRCoreServices(builder.Services, builder.Configuration);

            return builder;
        }

        /// <summary>
        /// Adds validators to the service collection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the validators to.</param>
        private static void AddValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateJobGradeRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateJobGradeRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<DeleteJobGradeRequestValidator>();
        }

        /// <summary>
        /// Adds Swagger services to the service collection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add Swagger services to.</param>
        private static void AddSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Adds OpenHRCore services to the service collection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="configuration">The configuration to use for adding services.</param>
        private static void AddOpenHRCoreServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenHRCoreWorkForceDbContext(configuration);
            services.AddOpenHRCoreWorkForceInfrastructure();
            services.AddOpenHRCoreWorkForceApplication();
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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
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
