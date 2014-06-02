using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MvcKatana2.Hubs
{
    [HubName("tickets")]
    [Authorize] //http://www.asp.net/signalr/overview/signalr-20/security/hub-authorization
    public class TicketHub : Hub
    {

    }
}
