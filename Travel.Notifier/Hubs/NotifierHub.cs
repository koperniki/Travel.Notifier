using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Travel.Notifier.Services;

namespace Travel.Notifier.Hubs
{
    public class NotifierHub : Hub
    {
        private readonly ButtonStateService _stateService;

        public NotifierHub(ButtonStateService stateService)
        {
            _stateService = stateService;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine(Context.ConnectionId);
            await Task.Delay(1000);
            await Clients.Client(Context.ConnectionId).SendAsync("transferstatedata", _stateService.GetButtons());

        }
    }
}