using Microsoft.AspNetCore.SignalR;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Collections.Concurrent;
using System.ComponentModel;

public class EmployeeInformationPlugin
{
    public class Employee
    {
        [Description("Full name of the employee.")]
        public string Name { get; set; } = string.Empty;

        [Description("Job title or position.")]
        public string Position { get; set; } = string.Empty;

        [Description("Age of the employee.")]
        public int Age { get; set; } = 16;
    }

    /// <summary>
    /// Retrieves all employee information.
    /// </summary>
    /// <returns>A list of employees with name, position, and age.</returns>
    [KernelFunction, Description("Retrieves the list of all employees including name, position, and age.")]
    public List<Employee> GetAllEmployees()
    {
        return new List<Employee>
        {
            new Employee { Name = "John Doe", Position = "Software Engineer", Age = 30 },
            new Employee { Name = "Jane Smith", Position = "Project Manager", Age = 35 },
            new Employee { Name = "Alice Johnson", Position = "UX Designer", Age = 28 }
        };
    }
}


public class ChatMessage
{
    public string User { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? ThinkText { get; set; }
    public bool ShowThink { get; set; } = false;
    public bool IsAgent => User == "AI";
}


public class ChatHub : Hub
{
    private readonly IChatCompletionService _chatCompletionService;
    private readonly Kernel _kernel;
    private static readonly ConcurrentDictionary<string, ChatHistory> UserSessions = new();

    private const string AI_USER = "AI";



    public ChatHub(IChatCompletionService chatCompletionService, Kernel kernel)
    {
        _chatCompletionService = chatCompletionService;
        _kernel = kernel;
    }

    public async Task SendMessage(string user, string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        var connectionId = Context.ConnectionId;
        var chatHistory = UserSessions.GetOrAdd(connectionId, _ => new ChatHistory());
        OpenAIPromptExecutionSettings settings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        chatHistory.AddUserMessage(message);

        await Clients.Caller.SendAsync("ReceiveMessage", user, message);

        // Use the _kernel instance instead of the Kernel type
        var response = _chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory: chatHistory, settings, _kernel);
        string fullResponse = string.Empty;

        await foreach (var chunk in response)
        {
            if (!string.IsNullOrEmpty(chunk?.Content))
            {
                fullResponse += chunk.Content;
                await Clients.Caller.SendAsync("ReceiveStreamedToken", AI_USER, chunk.Content);
            }
        }

        chatHistory.AddAssistantMessage(fullResponse);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        UserSessions.TryRemove(Context.ConnectionId, out _);
        return base.OnDisconnectedAsync(exception);
    }
}


