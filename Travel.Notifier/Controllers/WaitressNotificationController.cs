using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Travel.Notifier.Hubs;

namespace Travel.Notifier.Controllers
{
    public class WaitressNotificationController : Controller
    {
        private readonly IHubContext<NotifierHub> _hubContext;

        public WaitressNotificationController(IHubContext<NotifierHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("/Signal/{id}")]
        public async Task<IActionResult> Signal(string id)
        {
            await _hubContext.Clients.All.SendAsync("newSignal", id);
            return Ok();
        }
    }
}