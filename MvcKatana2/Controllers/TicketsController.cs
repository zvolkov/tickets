using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.SignalR.Infrastructure;
using MvcKatana2.Hubs;
using MvcKatana2.Models;
using NPoco;

namespace MvcKatana2.Controllers
{
    public class TicketsController : ApiController
    {
        private readonly IDatabase _db;
        readonly Microsoft.AspNet.SignalR.IHubContext _hub;

        public TicketsController(IDatabase db, IConnectionManager signalR)
        {
            _db = db;
            _hub = signalR.GetHubContext<TicketHub>();
        }

        [HttpPost]
        public IEnumerable<Ticket> GetAll()
        {
            return _db.Fetch<Ticket>("select * from tickets");
        }

        [HttpPost]
        public void ChangeState(Ticket t)
        {
            var ticket = _db.Single<Ticket>("select * from tickets where id=@0", t.id);

            ticket.state = t.state;
            _db.Update("tickets", "id", ticket);
            _hub.Clients.All.updated(ticket);
        }
    }
}
