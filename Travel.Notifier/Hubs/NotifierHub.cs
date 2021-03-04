using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Travel.Notifier.Services;

namespace Travel.Notifier.Hubs
{
    public class NotifierHub : Hub
    {
        private readonly ButtonStateService _stateService;
        private readonly GameStateService _gameService;

        public NotifierHub(ButtonStateService stateService, GameStateService gameService)
        {
            _stateService = stateService;
            _gameService = gameService;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine(Context.ConnectionId);
            await Task.Delay(1000);
            await Clients.Client(Context.ConnectionId).SendAsync("transferstatedata", _stateService.GetButtons());
            await Clients.Client(Context.ConnectionId).SendAsync("transfergamedata", _gameService.GetButtons());

        }
    }
}