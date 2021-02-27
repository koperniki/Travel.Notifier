using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Travel.Notifier.Hubs
{
    public class NotifierHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
             Console.WriteLine(Context.ConnectionId);
             await base.OnConnectedAsync();
        }
    }
}