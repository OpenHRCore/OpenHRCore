using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.AI;
using OllamaSharp;

public class ChatHub : Hub
{
    private readonly IChatClient client;

    public ChatHub()
    {
        client = new OllamaApiClient(
            new Uri("http://localhost:11434/"), "llama3.1:8b");
    }

    public async Task SendMessage(string user, string message)
    {
        user = "StephenKyaw";

        // Send user's message
        await Clients.All.SendAsync("ReceiveMessage", user, message);

        // Stream AI response token-by-token
        await foreach (var response in client.GetStreamingResponseAsync(message))
        {
            await Clients.All.SendAsync("ReceiveStreamedToken", "AI", response.Text);
        }
    }
}
