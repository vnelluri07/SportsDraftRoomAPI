using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SportsDraftRoom.Hubs;

/// <summary>
/// Creating a SignalR hub for chat functionality to test and understand the SignalR library.
/// </summary>

//TODO: will need to add authorization later
//[Authorize]
public class ChatHub : Hub
{
    //Any client can call this method to send a message to all connected clients
    public Task SendMessage(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}

