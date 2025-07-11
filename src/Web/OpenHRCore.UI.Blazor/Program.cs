using Microsoft.AspNetCore.ResponseCompression;
using MudBlazor;
using MudBlazor.Services;
using OllamaSharp;
using OpenHRCore.UI.Blazor.Shared;

namespace OpenHRCore.UI.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddChatClient(service =>
                new OllamaApiClient("http://localhost:11434/", "deepseek-r1:14b"));

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
