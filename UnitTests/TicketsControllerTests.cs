using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using MvcKatana2.Controllers;
using MvcKatana2.Hubs;
using MvcKatana2.Models;
using NPoco;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace UnitTests
{
    [TestFixture]
    public class TicketsControllerTests
    {
        private TicketsController sut;
        private IDatabase db;
        private IConnectionManager connectionManager;
        private IHubContext hubContext;

        [SetUp]
        public void Initialize()
        {
            db = Substitute.For<IDatabase>();
            
            connectionManager = Substitute.For<IConnectionManager>();
            hubContext = Substitute.For<IHubContext>();
            connectionManager.GetHubContext<TicketHub>().Returns(hubContext);

            sut = new TicketsController(db, connectionManager);
        }

        [Test]
        public void GetAll()
        {
            //GIVEN a list of tickets
            var list = new List<Ticket> {new Ticket(), new Ticket(), new Ticket()};
            db.Fetch<Ticket>(Arg.Any<string>()).Returns(list);

            //WHEN sut is asked for the list of tickets
            var result = sut.GetAll();

            //THEN it returns them all
            result.ShouldBe(list);
        }

        [Test]
        public void ChangeState()
        {
            //GIVEN a bg in a pending state
            var ticket = new Ticket{id = 4, state = "pending"};
            db.Single<Ticket>(Arg.Any<string>(), 4).Returns(ticket);

            //GIVEN a signalR client proxy
            //after http://stackoverflow.com/a/16008194/76859
            var clientProxy = Substitute.For<IClientProxy>();
            SubstituteExtensions.Returns(hubContext.Clients.All, clientProxy);
            
            //WHEN sut is asked to change the status to "done"
            sut.ChangeState(new Ticket {id = 4, state = "done"});

            //THEN the original item should get updated
            db.Received().Update("tickets", "id", ticket);
            ticket.state.ShouldBe("done");

            //and the clients should get notified
            clientProxy.Received().updated(ticket);
        }

        public interface IClientProxy
        {
            void updated(Ticket ticket);
        }
    }
}
