using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SoftServe.ITAcademy.BackendDubbingProject.Streaming.Core.Hubs
{
    internal class StreamHub : Hub
    {
        private static int _count;
        private static bool _needWait;
        private string _adminId;

        public override async Task OnConnectedAsync()
        {
            _count++;

            await base.OnConnectedAsync();

            var numberOfConnections = (_count - 1).ToString();

            await Clients.User(_adminId).SendAsync("updateCount", numberOfConnections);

            if (_needWait)
                await Clients.Caller.SendAsync("ReceiveMessage", "Start");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _count--;

            await base.OnDisconnectedAsync(exception);

            var numberOfConnections = (_count - 1).ToString();

            await Clients.User(_adminId).SendAsync("updateCount", numberOfConnections);
        }

        public async Task SendMessage(string message)
        {
            switch (message)
            {
                case "Start":
                    _adminId = Context.ConnectionId;
                    _needWait = true;
                    break;
                case "End":
                    _adminId = null;
                    _needWait = false;
                    break;
                default:
                    _needWait = false;
                    break;
            }

            await Clients.Others.SendAsync("ReceiveMessage", message);
        }
    }
}