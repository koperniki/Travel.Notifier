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

        public WaitressNotificationController(IHubContext<NotifierHub> hubContext, ButtonStateService stateService)
        {
            _hubContext = hubContext;
            _stateService = stateService;
        }

        [HttpGet("/issue/{id}")]
        public async Task<IActionResult> Issue(int id)
        {
            _stateService.UpdateState(id, true);
            Console.WriteLine($"sendedUp {id}");
            await _hubContext.Clients.All.SendAsync("transferstatedata", _stateService.GetButtons());
            return Ok();
        }

        [HttpGet("/release/{id}")]
        public async Task<IActionResult> Release(int id)
        {
            _stateService.UpdateState(id, false);
            Console.WriteLine($"sendedDown {id}");
            await _hubContext.Clients.All.SendAsync("transferstatedata", _stateService.GetButtons());
            return Ok();
        }
    }
}