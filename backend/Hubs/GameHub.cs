using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace backend.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendDrawing(string user, float x, float y, string color)
        {
            await Clients.Others.SendAsync("ReceiveDrawing", user, x, y, color);
        }
    }
}