using Microsoft.AspNetCore.SignalR;
using RombiBack.Abstraction;
using RombiBack.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.Hubs
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task NotificarATodos(string mensaje)
        {
            return _hubContext.Clients.All.SendAsync("ReceiveNotification", "Sistema", mensaje);
        }
    }
}
