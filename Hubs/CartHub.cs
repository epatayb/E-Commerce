using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace dotnet_store.Hubs;

public class CartHub : Hub
{
    public async Task UpdateQuantity(string userId)
    {
        await Clients.All.SendAsync("CartUpdated", userId);
    }
    public async Task NotifyQuantityChanged( string userId, int productId, int quantity)
    {
        await Clients.User(userId).SendAsync("QuantityChanged", productId, quantity);
    }
}