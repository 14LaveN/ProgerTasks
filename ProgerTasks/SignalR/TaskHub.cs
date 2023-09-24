using Microsoft.AspNetCore.SignalR;

namespace ProgerTasks.SignalR;

public class TaskHub : Hub
{
    public async Task SendMessage(string teamName, string message)
    {
        await Clients.Client(teamName).SendAsync("Receive", message);
    }
}