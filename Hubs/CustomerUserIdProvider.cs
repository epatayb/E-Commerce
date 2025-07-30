using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
public class CustomerUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.Identity?.Name;
    }
} 