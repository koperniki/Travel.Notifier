using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Travel.Notifier.Hubs;
using Travel.Notifier.Services;

namespace Travel.Notifier.Controllers
{
    public class WaitressNotificationController : Controller
    {
        private readonly IHubContext<NotifierHub> _hubContext;
        private readonly ButtonStateService _stateService;
        private readonly GameStateService _gameService;

        public WaitressNotificationController(IHubContext<NotifierHub> hubContext, ButtonStateService stateService, GameStateService gameService)
        {
            _hubContext = hubContext;
            _stateService = stateService;
            _gameService = gameService;
        }

        [HttpGet("/issue/{id}")]
        public async Task<IActionResult> Issue(int id)
        {
            if (id < 1 || id > 6)
            {
                return Ok();
            }
            _stateService.UpdateState(id, true);
            _gameService.UpdateState(id, true);
            Console.WriteLine($"sendedUp {id}");
            await _hubContext.Clients.All.SendAsync("transferstatedata", _stateService.GetButtons());
            await _hubContext.Clients.All.SendAsync("transfergamedata", _gameService.GetButtons());
            return Ok();
        }

        [HttpGet("/release/{id}")]
        public async Task<IActionResult> Release(int id)
        {
            if (id < 1 || id > 6)
            {
                return Ok();
            }
            _stateService.UpdateState(id, false);
            Console.WriteLine($"sendedDown {id}");
            await _hubContext.Clients.All.SendAsync("transferstatedata", _stateService.GetButtons());
            return Ok();
        }

        [HttpGet("/next")]
        public async Task<IActionResult> Next()
        {
            _gameService.ClearState();
            await _hubContext.Clients.All.SendAsync("transfergamedata", _gameService.GetButtons());
            return Ok();
        }
    }
}