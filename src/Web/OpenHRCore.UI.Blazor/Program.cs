using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.SemanticKernel;
using MudBlazor;
using MudBlazor.Services;
using OpenHRCore.UI.Blazor.Shared;

namespace OpenHRCore.UI.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var openAIConfig = builder.Configuration.GetSection("OpenAI");

            // Validate configuration values to avoid null reference issues
            var modelId = openAIConfig["ModelId"];
            var apiKey = openAIConfig["ApiKey"];

            if (string.IsNullOrEmpty(modelId))
            {
                throw new InvalidOperationException("OpenAI ModelId is not configured.");
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI ApiKey is not configured.");
            }

            // Add OpenAI Chat Completion Service
            builder.Services.AddKernel();
            builder.Services.AddOpenAIChatCompletion(modelId: modelId, apiKey: apiKey);
            builder.Services.AddScoped(sp => KernelPluginFactory.CreateFromType<EmployeeInformationPlugin>(serviceProvider: sp));
           

            // Add MudBlazor services
            builder.Services.AddMudServices();
            builder.Services.AddMudMarkdownServices();

           

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSignalR();

            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    ["application/octet-stream"]);
            });
            var app = builder.Build();

            app.UseResponseCompression();

            app.UseRouting();

            app.MapHub<ChatHub>("/chathub");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
