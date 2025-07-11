using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.AI;
using System.Collections.Concurrent;

public class ChatHub : Hub
{

    private readonly IChatClient _client;
    private static readonly ConcurrentDictionary<string, UserChatSession> _userSessions = new();

    public ChatHub(IChatClient client)
    {
        _client = ChatClientBuilderChatClientExtensions
            .AsBuilder(client)
            .UseFunctionInvocation()
            .Build();
    }


    public async Task SendMessage(string user, string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        var session = _userSessions.GetOrAdd(Context.ConnectionId, _ => new UserChatSession
        {
            ConversationId = Guid.NewGuid().ToString()
        });

        session.History.Add(new ChatMessage(ChatRole.User, message));

        await Clients.Caller.SendAsync("ReceiveMessage", user, message);

        string responseText = string.Empty;

        var chatOptions = new ChatOptions
        {
            ConversationId = session.ConversationId
        };

        await foreach (var response in _client.GetStreamingResponseAsync(session.History, chatOptions))
        {
            await Clients.Caller.SendAsync("ReceiveStreamedToken", "AI", response.Text);
            responseText += response.Text;
        }

        session.History.Add(new ChatMessage(ChatRole.Assistant, responseText));
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _userSessions.TryRemove(Context.ConnectionId, out _);
        return base.OnDisconnectedAsync(exception);
    }

    private class UserChatSession
    {
        public string ConversationId { get; set; } = string.Empty;
        public List<ChatMessage> History { get; set; } = new();
    }
}